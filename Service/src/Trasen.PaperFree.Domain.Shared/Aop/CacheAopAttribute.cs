using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Trasen.Caching.Redis;

namespace Trasen.PaperFree.Aop
{
    public class CacheAopAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 缓存秒数
        /// </summary>
        public int ExpireSeconds { get; set; } = 30;

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            //判断是否是异步方法
            bool isAsync = context.IsAsync();

            //先判断方法是否有返回值，无就不进行缓存判断
            Type? methodReturnType = context.GetReturnParameter().Type;
            if (methodReturnType == typeof(void) || methodReturnType == typeof(Task) || methodReturnType == typeof(ValueTask))
            {
                await next(context);
                return;
            }
            Type returnType = methodReturnType!;
            if (isAsync)
            {
                //取得异步返回的类型
                returnType = returnType.GenericTypeArguments.FirstOrDefault()!;
            }
            //获取方法参数名
            string param = JsonConvert.SerializeObject(context.Parameters);

            //获取方法名称，也就是缓存key值
            string key = "Methods:" + context.ImplementationMethod.DeclaringType!.FullName + "." + context.ImplementationMethod.Name;
            var cache = context.ServiceProvider.GetRequiredService<IRedisService>();
            //如果缓存有值，那就直接返回缓存值
            if (await cache.HExistsAsync(key, param))
            {
                var value = await cache.HGetAsync(key, param);
                if (isAsync)
                {
                    //判断是Task还是ValueTask
                    if (methodReturnType == typeof(Task<>).MakeGenericType(returnType!))
                    {
                        dynamic? _result = JsonConvert.DeserializeObject(value, returnType);
                        context.ReturnValue = (typeof(Task).IsAssignableFrom(methodReturnType)) ? Task.FromResult(_result) : _result;
                    }
                    else if (methodReturnType == typeof(ValueTask<>).MakeGenericType(returnType))
                    {
                        //反射构建ValueTask<>类型的返回值，相当于new ValueTask(value)
                        context.ReturnValue = Activator.CreateInstance(typeof(ValueTask<>).MakeGenericType(returnType), value);
                    }
                }
                else
                {
                    context.ReturnValue = value;
                }
                return;
            }
            await next(context);
            object returnValue;
            if (isAsync)
            {
                returnValue = await context.UnwrapAsyncReturnValue();
                //反射获取异步结果的值，相当于(context.ReturnValue as Task<>).Result
                //returnValue = typeof(Task<>).MakeGenericType(returnType).GetProperty(nameof(Task<object>.Result)).GetValue(context.ReturnValue);
            }
            else
            {
                returnValue = context.ReturnValue;
            }
            await cache.HSetAsync(key, param, returnValue);
            if (ExpireSeconds > 0)
            {
                await cache.ExpireAsync(key, TimeSpan.FromSeconds(ExpireSeconds));
            }
        }
    }
}
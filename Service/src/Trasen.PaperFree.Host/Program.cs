using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using Trasen.PaperFree.Domain.Shared.Appsettings;
using Trasen.PaperFree.Domain.Shared.Config;
using Trasen.PaperFree.Domain.Shared.Jsons;
using Trasen.PaperFree.Domain.Shared.Response;
using Trasen.PaperFree.Host.Filter;
using Trasen.PaperFree.Host.Middleware;
using Trasen.PaperFree.Infrastructure.SeedWork;
using Trasen.PaperFree.Infrastructure.SignalR;

namespace Trasen.PaperFree.Host;

/// <summary>
///
/// </summary>
public class Program
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task Main(string[] args)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.White;
            await Console.Out.WriteLineAsync("无纸化API服务初始化中....");
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpContextAccessor();
            //关闭系统自带的模型验证过滤器
            builder.Services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);

            builder.Services.AddControllers(options => options
            .Filters.Add(typeof(GlobalActionMonitor))).AddNewtonsoftJson(options =>
            {
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //设置本地时间而非UTC时间
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.RunModuleInitializers(AssemblyList());

            builder.Services.RunCachingRedisModuleInitializer();

            builder.Services.AddOptions();

            var configureOptions = Appsetting.Instance.GetSection("DbConfig");
            builder.Services.Configure<DbConnectionOption>(configureOptions).AddScoped<SlaveRoundRobin>().AddDbContext<AppDbContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.Configure<DbConnectionOption>(configureOptions).AddScoped<SlaveRoundRobin>().AddDbContext<BasicDataDbContext>();

            ConfigureAuthentication(builder);
            SwaggerGen(builder);
            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelsExpandDepth(-1);
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "无纸化 API");
            });
            //使用全局异常中间件
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                if (context.Request.Query.TryGetValue("access_token", out var token))
                {
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
                return next();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            ////配置hangfire仪表盘
            ConfigHangfireDashboard(app);
            app.MapHub<PersonHub>("/hub");
            app.MapControllers();
            Console.ForegroundColor = ConsoleColor.Green;
            await Console.Out.WriteLineAsync("后台服务启动已完成");
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync("服务启动异常");
            await Console.Out.WriteLineAsync($"异常错误:{ex.Message}");
        }
        finally
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    /// <summary>
    /// SwaggerGen
    /// </summary>
    /// <param name="builder"></param>
    private static void SwaggerGen(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "后端管理API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            //添加安全要求
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme{
                    Reference =new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id ="Bearer"
                    }
                },new string[]{ }
            }});
            //增强swagger接口文档注释
            var xmlApiPath = Path.Combine(AppContext.BaseDirectory, "Trasen.PaperFree.Host.xml");
            c.IncludeXmlComments(xmlApiPath, true);
            var xmlModelPath = Path.Combine(AppContext.BaseDirectory, "Trasen.PaperFree.Application.xml");
            c.IncludeXmlComments(xmlModelPath, true);
            // 开启加权小锁
            c.OperationFilter<AddResponseHeadersFilter>();
            c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        });
    }

    /// <summary>
    /// Hangfire仪表盘设置
    /// </summary>
    /// <param name="app"></param>
    private static void ConfigHangfireDashboard(WebApplication app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[]  { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                    {
                        SslRedirect = false,
                        RequireSsl = false,
                        LoginCaseSensitive = true,
                        Users = new[]
                        {
                            new BasicAuthAuthorizationUser
                            {
                                Login ="hangfire",
                                PasswordClear ="123456"
                            }
                        },
                    })
                },
            IgnoreAntiforgeryToken = true,//这里一定要写true 不然用client库写代码添加webjob会出错
            AppPath = "/hangfire-read",//返回时跳转的地址
            DisplayStorageConnectionString = false,//是否显示数据库连接信息
            IsReadOnlyFunc = Context => false
        });
    }

    private static void ConfigureAuthentication(WebApplicationBuilder context)
    {
        var jwtconfig = context.Configuration.GetSection("JWT").Get<JwtSetting>();

        #region 身份认证授权

        context.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                SaveSigninToken = true,//保存token,后台验证token是否生效(重要)
                ValidateIssuer = true,//是否验证Issuer
                ValidateAudience = true,//是否验证Audience
                ValidateLifetime = true,//是否验证失效时间
                ValidateIssuerSigningKey = true,//是否验证SecurityKey
                ValidAudience = jwtconfig.AccessTokenAudience,//接收
                ValidIssuer = jwtconfig.Issuer,//发行
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtconfig.SecurityKey))//密钥
            };
            options.Events = new JwtBearerEvents()
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.Clear();
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 401;
                    context.Response.WriteAsync(new ApiResult<string>(MessageType.Error, ResultCode.DENY, "授权未通过", default,false).ToJsonString());
                    return Task.CompletedTask;
                }
            };
        });

        #endregion 身份认证授权
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<Assembly> AssemblyList()
    {
        yield return Assembly.Load("Trasen.PaperFree.Application");
        yield return Assembly.Load("Trasen.PaperFree.Domain");
        yield return Assembly.Load("Trasen.PaperFree.Infrastructure");
    }
}
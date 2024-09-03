using System.ComponentModel;
using System.Reflection;
using Trasen.PaperFree.Domain.Shared.Attribute;
using Trasen.PaperFree.Domain.Shared.Attributes;

namespace Trasen.PaperFree.Domain.Shared.Help
{
    public class EnumberHelper
    {
        public static List<EnumberEntity> EnumToList<T>()
        {
            List<EnumberEntity> list = new List<EnumberEntity>();

            foreach (var e in System.Enum.GetValues(typeof(T)))
            {
                EnumberEntity m = new EnumberEntity();
                object[] objArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr != null && objArr.Length > 0)
                {
                    DescriptionAttribute da = objArr[0] as DescriptionAttribute;
                    m.Desction = da.Description;
                }
                m.EnumValue = Convert.ToInt32(e);
                m.EnumName = e.ToString();
                list.Add(m);
            }
            return list;
        }

        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static string GetDescription(System.Enum en)
        {
            Type type = en.GetType();   //获取类型
            MemberInfo[] memberInfos = type.GetMember(en.ToString());   //获取成员
            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];   //获取描述特性

                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;    //返回当前描述
                }
            }
            return en.ToString();
        }

        /// <summary>
        ///枚举
        /// </summary>
        /// <returns></returns>
        public static List<EnumberEntity> GetEnumType<T>()
        {
            var list = new List<EnumberEntity>();
            var type = typeof(T);
            var dic = Enum.GetValues(type!)?.Cast<object>().ToDictionary(enumValue => enumValue.ToString()!, enumValue => (int)enumValue);
            if(dic is null) return list;
            foreach (var e in dic)
            {
                var name = e.Key;
                var Description = type?.GetField(name??string.Empty)?.GetCustomAttribute<DescriptionAttribute>();
                var Parent = type?.GetField(name??string.Empty)?.GetCustomAttribute<EnumParentAttribute>();
                var ParentId = Parent == null ? new List<int>() : Parent.Parent;
                var Text = Description == null ? e.Key : Description.Description;
                ParentId.ForEach(item =>
                {
                    list.Add(new EnumberEntity
                    {
                        EnumValue = e.Value,
                        EnumName = name??string.Empty,
                        Desction = Text ?? string.Empty,
                        ParentId = item,
                    });
                });
            }
            return list;
        }



        /// <summary>
        ///枚举
        /// </summary>
        /// <returns></returns>
        public static List<EnumberEntity> GetEnumSortType<T>()
        {
            var list = new List<EnumberEntity>();
            var type = typeof(T);
            var dic = Enum.GetValues(type!)?.Cast<object>().ToDictionary(enumValue => enumValue.ToString()!, enumValue => (int)enumValue);
            if (dic is null) return list;
            foreach (var e in dic)
            {
                var name = e.Key;
                var Description = type?.GetField(name ?? string.Empty)?.GetCustomAttribute<DescriptionAttribute>();
                var Sort = type?.GetField(name ?? string.Empty)?.GetCustomAttribute<EnumSortAttribute>();
                var Text = Description == null ? e.Key : Description.Description;

                list.Add(new EnumberEntity
                {
                    EnumValue = e.Value,
                    EnumName = name ?? string.Empty,
                    Desction = Text ?? string.Empty,
                    Sort = Sort == null ? 0 : Sort.Sort
                });
            }
            return list;
        }

    }

    public class EnumberEntity
    {
        /// <summary>
        /// 枚举的描述
        /// </summary>
        public string Desction { set; get; }

        /// <summary>
        /// 枚举名称
        /// </summary>
        public string EnumName { set; get; }

        /// <summary>
        /// 枚举对象的值
        /// </summary>
        public int EnumValue { set; get; }

        /// <summary>
        /// 父级值
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
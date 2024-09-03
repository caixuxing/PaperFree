using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PaperFree.Client.Utils
{
    public static class CommonUtil
    {

        /// <summary>
        /// 对象生成Url参数字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToUrlParams(this object obj)
        {

            return string.Join("&", obj.GetType().GetProperties()
                .Select(x => new { x.Name, value = x.GetValue(obj, null) })
                .Where(x => x.value != null)
                .Select(x => $"{x.Name}={HttpUtility.UrlEncode(x.value.ToString())}"));

        }
    }
}

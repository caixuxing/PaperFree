using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperFree.Client.Utils
{
    /// <summary>
    /// 全局统一的缓存类
    /// </summary>
    public class Cache
    {
        private SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
        private static volatile Cache instance = null;
        private static object lockHelper = new object();

        private Cache()
        {

        }
        public void Add(string key, string value)
        {
            dic.Add(key, value);
        }
        public void Remove(string key)
        {
            dic.Remove(key);
        }

        public string Get(string key)
        {
            var data= dic[key];
            return data;
        }

        public string this[string index]
        {
            get
            {
                if (dic.ContainsKey(index))
                    return dic[index];
                else
                    return null;
            }
            set { dic[index] = value; }
        }




        public static Cache Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new Cache();
                        }
                    }
                }
                return instance;
            }
        }
    }
}

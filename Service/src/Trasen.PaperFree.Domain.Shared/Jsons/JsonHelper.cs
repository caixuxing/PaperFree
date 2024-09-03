using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Trasen.PaperFree.Domain.Shared.Jsons
{
    public static class JsonHelper
    {
        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(this T t) where T : class
        {
            if (t == null)
            {
                return string.Empty;
            }
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(t, Formatting.None, timeConverter);
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <param name="isThrowException">当序列化失败时，是否抛出异常</param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string jsonString, bool isThrowException = true) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception ex)
            {
                if (isThrowException)
                {
                    throw ex;
                }
                return null;
            }
        }

        /// <summary>
        /// 复制类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T CopyClass<T>(T source)
        {
            if (source == null) { return source; }
            string jsonString = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 将字典类型序列化为json字符串
        /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="dict">要序列化的字典数据</param>
        /// <returns>json字符串</returns>
        public static string SerializeDictionaryToJsonString<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            if (dict == null || dict.Count == 0)
                return "";

            string jsonStr = JsonConvert.SerializeObject(dict);
            return jsonStr;
        }

        /// <summary>
        /// 将json字符串反序列化为字典类型
        /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>字典数据</returns>
        public static Dictionary<TKey, TValue> DeserializeStringToDictionary<TKey, TValue>(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();

            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);

            return jsonDict;
        }

        /// <summary>
        /// 把obj1合并到obj2
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static JObject Merge(object obj1, object obj2)
        {
            if (obj1 == null && obj2 == null)
            {
                return null;
            }

            if (obj1 == null)
            {
                return JObject.FromObject(obj2);
            }
            if (obj2 == null)
            {
                return JObject.FromObject(obj1);
            }

            var jObj1 = JObject.FromObject(obj1);
            var jObj2 = JObject.FromObject(obj2);

            jObj2.Merge(jObj1, new JsonMergeSettings { MergeNullValueHandling = MergeNullValueHandling.Merge, MergeArrayHandling = MergeArrayHandling.Merge });

            return jObj2;
        }

        /// <summary>
        /// obj1的内容替换obj2
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static JObject Replace(object obj1, object obj2)
        {
            if (obj1 == null && obj2 == null)
            {
                return null;
            }

            if (obj1 == null)
            {
                return JObject.FromObject(obj2);
            }
            if (obj2 == null)
            {
                return JObject.FromObject(obj1);
            }

            var jObj1 = JObject.FromObject(obj1);
            var jObj2 = JObject.FromObject(obj2);

            jObj2.Merge(jObj1, new JsonMergeSettings { MergeNullValueHandling = MergeNullValueHandling.Merge, MergeArrayHandling = MergeArrayHandling.Replace });

            return jObj2;
        }

        /// <summary>
        /// 把obj1合并到obj2
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static Dictionary<string, object> Merge(Dictionary<string, object> obj1, Dictionary<string, object> obj2)
        {
            if (obj1 == null && obj2 == null)
            {
                return new Dictionary<string, object>();
            }

            if (obj1 == null)
            {
                return obj2;
            }
            if (obj2 == null)
            {
                return obj1;
            }

            foreach (var item in obj1)
            {
                obj2[item.Key] = item.Value;
            }

            return obj2;
        }

        /// <summary>
        /// 把obj1合并到obj2
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static JObject Merge(object obj1, object obj2, object obj3)
        {
            var mergeObj = Merge(obj1, obj2);
            mergeObj = Merge(mergeObj, obj3);

            return mergeObj;
        }

        /// <summary>
        /// 字节转换对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T Convert<T>(byte[] bytes)
        {
            var json = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static List<T> JsonEnDeserialize<T>(object data) where T : class
        {
            List<T> objData = new List<T>();
            try
            {
                if (data.ToString().Contains("["))
                {
                    var arry = JArray.Parse(data.ToString()).Children().ToList();
                    arry.ForEach((item) =>
                    {
                        objData.Add(JsonDeserialize<T>(item.ToString()));
                    });
                }
                else
                {
                    objData.Add(JsonDeserialize<T>(data.ToString()));
                }
            }
            catch (Exception ex)
            {
                objData.Add(JsonDeserialize<T>(data.ToString()));
            }

            return objData;
        }

        /// <summary>
        /// 获取对象字段值
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public static T GetValue<T>(JObject jObject, string expression)
        {
            try
            {
                string code = expression.Split('.').First();
                if (!jObject.ContainsKey(code))
                {
                    return default(T);
                }
                JToken value = jObject[code];

                expression = string.Join('.', expression.Split('.').Skip(1));
                if (string.IsNullOrEmpty(expression))
                {
                    return value.ToObject<T>();
                }

                if (value is JObject)
                {
                    jObject = value as JObject;
                }
                else if (value is JArray)
                {
                    var array = value as JArray;
                    if (array.FirstOrDefault() is JObject)
                    {
                        jObject = array.FirstOrDefault() as JObject;
                    }
                }
                if (jObject != null)
                {
                    return GetValue<T>(jObject, expression);
                }

                return default(T);
            }
            catch
            {
                //LogHelper.Error($"Json字段解析操作{expression}");
                throw;
            }
        }

        public static bool TryGetValue<T>(JObject jObject, string expression, out T value)
        {
            try
            {
                value = GetValue<T>(jObject, expression);
            }
            catch
            {
                value = default(T);
                return false;
            }
            return true;
        }

        /// <summary>
        ///索引库操作 获取组织架构主键Id
        /// </summary>
        /// <param name="jObject"></param>
        /// <param name="expression">xxx.Id</param>
        /// <returns></returns>
        public static List<Guid> TryGetValue(JObject jObject, string expression)
        {
            List<Guid> result = new List<Guid>();
            string code = expression.Split('.').First();
            if (jObject.ContainsKey(code))
            {
                JToken value = jObject.SelectToken(code);
                if (value is JObject)
                {
                    result.Add(Guid.Parse(jObject.SelectToken(expression).ToString()));
                }
                else if (value is JArray)
                {
                    var array = value as JArray;
                    var two_code = expression.Split('.')[1].ToString();
                    foreach (var item in array)
                    {
                        result.Add(Guid.Parse(item.SelectToken(two_code).ToString()));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取对象字段
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public static JToken GetJToken(JObject jObject, string expression)
        {
            string code = expression.Split('.').First();
            if (!jObject.ContainsKey(code))
            {
                return null;
            }
            JToken value = jObject[code];

            expression = string.Join('.', expression.Split('.').Skip(1));
            if (string.IsNullOrEmpty(expression))
            {
                return value;
            }
            JObject o = null;
            if (value is JObject)
            {
                o = value as JObject;
            }
            else if (value is JArray)
            {
                var array = value as JArray;
                if (array.FirstOrDefault() is JObject)
                {
                    o = array.FirstOrDefault() as JObject;
                }
            }
            if (o != null)
            {
                return GetJToken(o, expression);
            }

            return null;
        }

        #region 获取JArray内部的第一个JObject

        /// <summary>
        /// 获取JArray内部的第一个JObject
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static JObject GetFirstJObject(JArray array)
        {
            var value = array.FirstOrDefault();
            if (value == null)
            {
                return null;
            }
            else if (value is JObject)
            {
                return (JObject)value;
            }
            else if (value is JArray)
            {
                return GetFirstJObject(array);
            }
            return null;
        }

        #endregion 获取JArray内部的第一个JObject

        #region 获取表达式末端，返回所有末端值

        /// <summary>
        /// 获取表达式末端，返回所有末端值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jObject"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static List<T> GetValueList<T>(JObject jObject, string expression)
        {
            List<T> list = new List<T>();
            try
            {
                var tokens = GetJTokenList(jObject, expression);
                if (tokens.Count > 0)
                {
                    foreach (var token in tokens)
                    {
                        list.Add(token.ToObject<T>());
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        #endregion 获取表达式末端，返回所有末端值

        #region 获取表达式末端，返回所有JToken

        /// <summary>
        /// 获取表达式末端，返回所有JToken
        /// </summary>
        /// <param name="jObject"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static List<JToken> GetJTokenList(JObject jObject, string expression)
        {
            List<JToken> result = new List<JToken>();
            string code = expression.Split('.').First();
            if (!jObject.ContainsKey(code))
            {
                return result;
            }
            IEnumerable<JToken> jtokens = jObject.SelectTokens(code);

            expression = string.Join('.', expression.Split('.').Skip(1));
            if (string.IsNullOrEmpty(expression))
            {
                return jtokens.ToList();
            }

            foreach (JToken value in jtokens)
            {
                if (value is JObject)
                {
                    JObject o = value as JObject;
                    result.AddRange(GetJTokenList(o, expression));
                }
                else if (value is JArray)
                {
                    var array = value as JArray;
                    result.AddRange(GetJTokenList(array, expression));
                }
                else
                {
                    result.Add(value);
                }
            }
            return result;
        }

        /// <summary>
        /// 集合 返回集合JToken
        /// </summary>
        /// <param name="jArray"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static List<JToken> GetJTokenList(JArray jArray, string expression)
        {
            List<JToken> result = new List<JToken>();
            foreach (JToken jToken in jArray)
            {
                if (jToken is JObject)
                {
                    result.AddRange(GetJTokenList(jToken as JObject, expression));
                }
                else if (jToken is JArray)
                {
                    result.AddRange(GetJTokenList(jToken as JArray, expression));
                }
            }
            return result;
        }

        #endregion 获取表达式末端，返回所有JToken

        #region json 扩展

        /// <summary>
        /// 将对象转成JSON字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="lowerCase">是否小驼峰</param>
        /// <param name="indented">是否缩进</param>
        /// <param name="timeFormat">时间格式转换规则</param>
        /// <returns>JSON字符串</returns>
        public static string ToJsonString(this object obj, bool lowerCase = false, bool indented = false, string timeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            var settings = new JsonSerializerSettings()
            {
                DateFormatString = timeFormat
            };
            if (lowerCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }
            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// 将对象转成JSON字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="converters">JSON转换器</param>
        /// <param name="lowerCase">是否小驼峰</param>
        /// <param name="indented">是否缩进</param>
        /// <param name="timeFormat">时间格式转换规则</param>
        /// <returns>JSON字符串</returns>
        public static string ToJsonString(this object obj, IEnumerable<JsonConverter> converters, bool lowerCase = false, bool indented = false, string timeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            var settings = new JsonSerializerSettings()
            {
                DateFormatString = timeFormat
            };
            if (lowerCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }
            (settings.Converters as List<JsonConverter>).AddRange(converters);
            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// 将JSON字符串转成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">JSON字符串</param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
        }

        /// <summary>
        /// 将JSON字符串转成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">JSON字符串</param>
        /// <returns></returns>
        public static T FromObject<T>(this object value)
        {
            if (value != null)
            {
                if (value is string)
                {
                    return FromJsonString<T>(value.ToString());
                }
                else
                {
                    return FromJsonString<T>(JsonSerializer(value));
                }
            }
            return default(T);
        }

        /// <summary>
        /// 将JSON字符串转换为匿名类型
        /// </summary>
        /// <typeparam name="T">匿名类型</typeparam>
        /// <param name="json">JSON字符串 </param>
        /// <param name="anonymousTypeObject">匿名类型</param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string json, T anonymousTypeObject)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }
            return JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
        }

        /// <summary>
        /// 将JSON字符串转成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">JSON字符串 </param>
        /// <param name="converters">JSON转换器</param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string json, IEnumerable<JsonConverter> converters)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }
            var settings = new JsonSerializerSettings();
            (settings.Converters as List<JsonConverter>)?.AddRange(converters);
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        #endregion json 扩展
    }

    #region 把时间转成中国显示的格式

    public class ChinaDateTimeConverter : DateTimeConverterBase
    {
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd hh:mm:ss" };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return dtConverter.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dtConverter.WriteJson(writer, value, serializer);
        }
    }

    #endregion 把时间转成中国显示的格式
}
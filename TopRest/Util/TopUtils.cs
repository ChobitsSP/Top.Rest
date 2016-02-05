using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Top.Rest
{
    /// <summary>
    /// TOP系统工具类。
    /// </summary>
    public static class TopUtils
    {
        /// <summary>
        /// 给PUSH请求签名。
        /// </summary>
        /// <param name="parameters">所有字符型的PUSH请求参数</param>
        /// <param name="server_url">请求地址</param>
        /// <param name="secret">签名密钥</param>
        /// <returns>签名</returns>
        public static string SignTopRequest(IDictionary<string, string> parameters, string secret)
        {
            StringBuilder sb = new StringBuilder();
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();
            while (dem.MoveNext())
            {
                sb.Append(dem.Current.Key);
                sb.Append(UrlEncode("=" + dem.Current.Value));
            }
            sb.Append(secret);

            string query = sb.ToString();
            return GetMD5(query);
        }

        public static string UrlEncode(string value)
        {
            value = HttpUtility.UrlEncode(value, Encoding.UTF8);
            //value = value.Replace("+", "%20");
            value = System.Text.RegularExpressions.Regex.Replace(value, "(%[0-9a-f]{2})", c => c.Value.ToUpper());
            value = value.Replace("(", "%28").Replace(")", "%29").Replace("$", "%24").Replace("!", "%21").Replace("*", "%2A").Replace("'", "%27");
            //value = value.Replace("%7E", "~");
            return value;
        }

        public static string GetMD5(string query)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = bytes[i].ToString("X");
                if (hex.Length == 1)
                {
                    result.Append("0");
                }
                result.Append(hex);
            }
            return result.ToString();
        }

        public static T JsonParser<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToJson<T>(this T obj, bool lower = false)
        {
            var settings = new JsonSerializerSettings();
            var json = JsonConvert.SerializeObject(obj,
#if DEBUG
            Formatting.Indented
#else
            Formatting.None
#endif
, settings);
            return json;
        }

        /// <summary>
        /// 清除字典中值为空的项。
        /// </summary>
        /// <param name="dict">待清除的字典</param>
        /// <returns>清除后的字典</returns>
        public static IDictionary<string, T> CleanupDictionary<T>(IDictionary<string, T> dict)
        {
            IDictionary<string, T> newDict = new Dictionary<string, T>(dict.Count);
            IEnumerator<KeyValuePair<string, T>> dem = dict.GetEnumerator();

            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                T value = dem.Current.Value;
                if (value != null)
                {
                    newDict.Add(name, value);
                }
            }

            return newDict;
        }

        /// <summary>
        /// 获取从1970年1月1日到现在的毫秒总数。
        /// </summary>
        /// <returns>毫秒数</returns>
        public static long GetCurrentTimeMillis(DateTime time)
        {
            return (long)time.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        /// <summary>
        /// 获取unix时间戳。
        /// </summary>
        /// <returns>unix时间戳</returns>
        public static ulong GetUnixTimestamp(DateTime time)
        {
            return (ulong)time.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }
    }
}

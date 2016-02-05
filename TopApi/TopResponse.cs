using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Top.Rest
{
    public class TopResponse
    {
        public TopResponse() { }

        public TopResponse(int code, string msg)
        {
            this.ErrCode = code;
            this.ErrMsg = msg;
        }

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("code")]
        public int ErrCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("msg")]
        public string ErrMsg { get; set; }

        /// <summary>
        /// 响应原始内容
        /// </summary>
        [JsonIgnore]
        public string Body { get; set; }

        /// <summary>
        /// HTTP GET请求的URL
        /// </summary>
        [JsonIgnore]
        public string ReqUrl { get; set; }

        /// <summary>
        /// 响应结果是否错误
        /// </summary>
        [JsonIgnore]
        public bool IsError
        {
            get
            {
                return this.ErrCode != 0 || !string.IsNullOrEmpty(this.ErrMsg);
            }
        }
    }

    public class ResultResponse : TopResponse
    {
        public ResultResponse(object result)
        {
            if (result != null)
            {
                this.Result = JObject.FromObject(result);
            }
        }

        [JsonProperty("result")]
        internal JObject Result { get; set; }

        public T GetResult<T>()
        {
            return Result.ToObject<T>();
        }
    }
}

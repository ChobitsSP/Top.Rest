using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Top.Rest.Request
{
    /// <summary>
    /// TOP请求接口。
    /// </summary>
    public interface ITopRequest<T> : IParamsRequest where T : TopResponse
    {
        /// <summary>
        /// 获取TOP的API名称。
        /// </summary>
        /// <returns>API名称</returns>
        string GetApiName();

        /// <summary>
        /// 提前验证参数。
        /// </summary>
        void Validate();
    }

    /// <summary>
    /// formatter and parser
    /// </summary>
    public interface IParamsRequest
    {
        void SetParameters(NameValueCollection parameters);

        /// <summary>
        /// 获取所有的Key-Value形式的文本请求参数字典。其中：
        /// Key: 请求参数名
        /// Value: 请求参数文本值
        /// </summary>
        /// <returns>文本请求参数字典</returns>
        IDictionary<string, string> GetParameters();
    }
    
    public interface ITopCache
    {
        TopResponse Read(string key);
        void Write(string key, TopResponse rsp);
    }

    /// <summary>
    /// 可被缓存的请求
    /// </summary>
    public interface ICacheRequest
    {
        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GetKey(NameValueCollection parameters);

        /// <summary>
        /// 超时时间(毫秒)
        /// </summary>
        int Timeout { get; }
    }
}

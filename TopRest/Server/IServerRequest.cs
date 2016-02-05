using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Top.Rest.Request;

namespace Top.Rest.Server
{
    public interface IServerRequest<T, K> where T : ITopRequest<K> where K : TopResponse
    {
        K GetResponse(T request);
    }

    /// <summary>
    /// 验证参数 throw TopException
    /// </summary>
    public interface IParamsValidate
    {
        /// <summary>
        /// 验证参数 throw TopException
        /// </summary>
        void Validate(NameValueCollection parameters);
    }
}

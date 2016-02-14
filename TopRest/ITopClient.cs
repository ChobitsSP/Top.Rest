using System;
using Top.Rest.Request;

namespace Top.Rest
{
    /// <summary>
    /// TOP客户端。
    /// </summary>
    public interface ITopClient
    {
        /// <summary>
        /// 执行TOP公开API请求。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">具体的TOP API请求</param>
        /// <returns>领域对象</returns>
        T Execute<T>(ITopRequest<T> request) where T : TopResponse;

        /// <summary>
        /// 执行TOP隐私API请求。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">具体的TOP API请求</param>
        /// <param name="timestamp">请求时间戳</param>
        /// <returns>领域对象</returns>
        T Execute<T>(ITopRequest<T> request, DateTime timestamp) where T : TopResponse;

        /// <summary>
        /// 执行TOP公开API请求。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">具体的TOP API请求</param>
        /// <returns>领域对象</returns>
        T Execute<T>(IParamsRequest request) where T : TopResponse;

        /// <summary>
        /// 执行TOP隐私API请求。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">具体的TOP API请求</param>
        /// <param name="timestamp">请求时间戳</param>
        /// <returns>领域对象</returns>
        T Execute<T>(IParamsRequest request, DateTime timestamp) where T : TopResponse;
    }
}

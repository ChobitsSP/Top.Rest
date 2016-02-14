using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Top.Rest.Request;
using Top.Rest.Util;
using System.Text;

namespace Top.Rest
{
    /// <summary>
    /// 基于REST的TOP客户端。
    /// </summary>
    public class TopClient : ITopClient
    {
        private const string SERVER_URL = "https://channel.api.duapp.com/rest/2.0/channel/";
        public const string API_KEY = "apikey";
        private const string METHOD = "method";
        public const string TIMESTAMP = "timestamp";
        private const string VERSION = "v";
        public const string SIGN = "sign";
        private const string EXPIRES = "expires";

        /// <summary>
        /// 禁用响应结果解释
        /// </summary>
        public bool DisableParser { private get; set; }

        /// <summary>
        /// 禁用日志调试功能
        /// </summary>
        public bool DisableTrace { private get; set; }

        public string ServerUrl { get; private set; }
        public ITopLogger topLogger { get; set; }

        private string apiKey;
        private string appSecret;
        private WebUtils webUtils;

        #region DefaultTopClient Constructors

        public TopClient(string serverUrl, string appKey, string appSecret)
        {
            this.apiKey = appKey;
            this.appSecret = appSecret;
            this.ServerUrl = serverUrl;
            this.webUtils = new WebUtils();
            this.topLogger = new DefaultTopLogger();
        }

        #endregion

        public void SetTimeout(int timeout)
        {
            this.webUtils.Timeout = timeout;
        }

        #region ITopClient Members

        public T Execute<T>(ITopRequest<T> request) where T : TopResponse
        {
            return Execute<T>(request, DateTime.Now);
        }

        public T Execute<T>(ITopRequest<T> request, DateTime timestamp) where T : TopResponse
        {
            return Execute<T>(request as IParamsRequest, timestamp);
        }

        public T Execute<T>(IParamsRequest request) where T : TopResponse
        {
            return Execute<T>(request, DateTime.Now);
        }

        public T Execute<T>(IParamsRequest request, DateTime timestamp) where T : TopResponse
        {
            try
            {
                request.Validate();
            }
            catch (TopException e)
            {
                return createErrorResponse<T>(e.ErrorCode, e.ErrorMsg);
            }

            string url = this.ServerUrl;

            var txtParams = new TopDictionary(request.GetParameters());
            txtParams.Add(METHOD, request.GetApiName());
            txtParams.Add(API_KEY, apiKey);
            txtParams.Add(TIMESTAMP, TopUtils.GetUnixTimestamp(timestamp).ToString());
            //if (expires.HasValue) txtParams.Add(EXPIRES, PushUtils.GetUnixTimestamp(expires.Value).ToString());
            txtParams.Add(SIGN, TopUtils.SignTopRequest(txtParams, appSecret));

            string body;

            if (request is ITopUploadRequest)
            {
                var fileParams = (request as ITopUploadRequest).GetFileParameters();
                body = webUtils.DoPost(url, txtParams, fileParams);
            }
            else
            {
                body = webUtils.DoPost(url, txtParams);
            }

            // 解释响应结果
            T rsp = this.DisableParser ? Activator.CreateInstance<T>() : TopUtils.JsonParser<T>(body);
            rsp.Body = body;

            // 追踪错误的请求
            if (!DisableTrace)
            {
                txtParams.Remove(SIGN);
                txtParams.Add(SIGN, TopUtils.SignTopRequest(txtParams, appSecret));
                rsp.ReqUrl = WebUtils.BuildGetUrl(url, txtParams);
                if (rsp.IsError)
                {
                    topLogger.Warn(rsp.ReqUrl + "\r\n" + rsp.Body);
                }
            }

            return rsp;
        }

        #endregion

        private static T createErrorResponse<T>(int errCode, string errMsg) where T : TopResponse
        {
            T rsp = Activator.CreateInstance<T>();
            rsp.ErrCode = errCode;
            rsp.ErrMsg = errMsg;

            rsp.Body = rsp.ToJson();
            return rsp;
        }
    }
}

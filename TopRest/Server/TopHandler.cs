using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Top.Rest.Util;

namespace Top.Rest.Server
{
    public abstract class TopHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            string method = request.Params["method"];
            //string format = context.Request.QueryString["format"] ?? "json";

            string body;

            try
            {
                var type = GetParamsRequest(method);
                var rsp = request.GetTopResponse(type);
                body = rsp.ToJson();
            }
            catch (TopException ex)
            {
                LogException(ex);
                body = new TopResponse(ex.ErrorCode, ex.ErrorMsg).ToJson();
            }
            catch (Exception ex)
            {
                LogException(ex);
                body = new TopResponse(1, ex.Message).ToJson();
            }

            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.Write(body);
            context.Response.End();
        }

        //protected abstract IParamsRequest GetParamsRequest(string method);
        protected abstract Type GetParamsRequest(string method);
        protected virtual void LogException(Exception ex) { }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

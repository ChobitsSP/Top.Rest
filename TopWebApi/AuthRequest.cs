using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using Top.Rest;
using Top.Rest.Server;
using Top.Rest.Util;

namespace TopWebApi
{
    public class AuthRequest : IParamsValidate
    {
        public static readonly string APPID = ConfigurationManager.AppSettings["APPID"];
        public static readonly string APPSECRET = ConfigurationManager.AppSettings["APPSECRET"];

        public void Validate(NameValueCollection parameters)
        {
            ////appid验证
            string appid = parameters.ValidateRequired(TopClient.API_KEY);
            if (appid != APPID)
            {
                throw new TopException(102, TopClient.API_KEY + " error");
            }

            //允许客户端请求时间误差为6分钟。
            ulong timestamp = ulong.Parse(parameters.ValidateRequired(TopClient.TIMESTAMP));
            DateTime dt = TopUtils.UnixTimeStampToDateTime(timestamp);
            double totalMinutes = (DateTime.Now - dt).TotalMinutes;

            if (Math.Abs(totalMinutes) > 6)
            {
                throw new TopException(102, TopClient.TIMESTAMP + " error");
            }
            
            //签名验证
            string psign = parameters.ValidateRequired(TopClient.SIGN);
            var dic = parameters.ToDictionary();
            dic.Remove(TopClient.SIGN);
            string sign = TopUtils.SignTopRequest(dic, APPSECRET);

            if (!string.Equals(sign, psign, StringComparison.OrdinalIgnoreCase))
            {
                throw new TopException(102, TopClient.SIGN + " error");
            }
        }
    }
}

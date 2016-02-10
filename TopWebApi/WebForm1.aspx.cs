using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Top.Rest;
using Top.Rest.Domain;
using Top.Rest.Request;

namespace TopWebApi
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public static readonly string APPID = ConfigurationManager.AppSettings["APPID"];
        public static readonly string APPSECRET = ConfigurationManager.AppSettings["APPSECRET"];

        protected void Page_Load(object sender, EventArgs e)
        {
            string serverurl = GetSiteRoot() + "/TopHandler.ashx";

            var client = new TopClient(serverurl, APPID, APPSECRET);

            var req = new DemoRequest();

            req.p1 = new DemoObject()
            {
                id = 10,
                name = "hello world!",
            };

            var rsp = client.Execute(req);

            System.Diagnostics.Debugger.Break();
        }

        public static string GetSiteRoot()
        {
            string port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (port == null || port == "80" || port == "443")
                port = "";
            else
                port = ":" + port;

            string protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (protocol == null || protocol == "0")
                protocol = "http://";
            else
                protocol = "https://";

            string sOut = protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + System.Web.HttpContext.Current.Request.ApplicationPath;

            if (sOut.EndsWith("/"))
            {
                sOut = sOut.Substring(0, sOut.Length - 1);
            }

            return sOut;
        }
    }
}
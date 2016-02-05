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
            string serverurl = "http://localhost:50065/TopHandler.ashx";

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
    }
}
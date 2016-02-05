using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Top.Rest.Domain;
using Top.Rest.Response;
using Top.Rest.Util;

namespace Top.Rest.Request
{
    public class DemoRequest : ITopRequest<DemoResponse>
    {
        public DemoObject p1 { get; set; }

        public string GetApiName()
        {
            return "demo";
        }

        public IDictionary<string, string> GetParameters()
        {
            var dic = new TopDictionary();
            dic.Add("p1", p1.ToJson());
            return dic;
        }

        public void SetParameters(NameValueCollection parameters)
        {
            this.p1 = parameters.ValidateJson<DemoObject>("p1");
        }

        public void Validate()
        {
            return;
            throw new NotImplementedException();
        }
    }
}

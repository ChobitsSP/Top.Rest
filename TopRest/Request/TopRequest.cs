using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Top.Rest.Util;

namespace Top.Rest.Request
{
    public abstract class TopRequest<T> : ITopRequest<T> where T : TopResponse
    {
        private const string JSON_NAME = "JSON";

        public abstract string GetApiName();

        public virtual IDictionary<string, string> GetParameters()
        {
            var dic = new Dictionary<string, string>();
            dic.Add(JSON_NAME, this.ToJson());
            return dic;
        }

        public virtual void SetParameters(NameValueCollection parameters)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(parameters[JSON_NAME], this.GetType());

            foreach (var p in this.GetType().GetProperties())
            {
                var value = p.GetValue(obj, null);
                p.SetValue(this, value, null);
            }
        }

        public virtual void Validate()
        {
        }
    }
}

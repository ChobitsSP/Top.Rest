using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Top.Rest.Response
{
    public class DemoResponse : TopResponse
    {

        public DateTime time { get; set; }

        public int id { get; set; }

        public string name { get; set; }
    }
}

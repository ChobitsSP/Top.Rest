using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Rest;
using Top.Rest.Request;
using Top.Rest.Response;
using Top.Rest.Server;

namespace TopWebApi.Api
{
    public class demo : AuthRequest, IServerRequest<DemoRequest, DemoResponse>
    {
        public DemoResponse GetResponse(DemoRequest request)
        {
            var d = request.p1;

            System.Diagnostics.Debugger.Break();

            d.id = (int)DateTime.Now.Ticks;
            d.name = "new name";
            
            return new DemoResponse()
            {
                time = DateTime.Now,
                id = d.id,
                name = d.name,
            };
        }
    }
}

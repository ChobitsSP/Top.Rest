using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using Top.Rest.Request;

namespace Top.Rest.Server
{
    public static class ReflectionUtils
    {
        public static TopResponse GetRequestType<T>(T req, NameValueCollection parameters) where T : class
        {
            var serverReq = GetGenericServerRequest(req.GetType());
            if (serverReq == null)
            {
                throw new TopException(1, "method is null");
            }

            var reqType = serverReq.GetGenericArguments().First();
            var preq = Activator.CreateInstance(reqType);
            (preq as IParamsRequest).SetParameters(parameters);
            var func = serverReq.GetMethods().First();

            try
            {
                var rsp = (TopResponse)func.Invoke(req, new object[] { preq });
                return rsp;
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        static readonly Type GenericServerRequestType = typeof(IServerRequest<,>);
        public static Type GetGenericServerRequest(Type type)
        {
            if (type.IsGenericType && GenericServerRequestType.IsAssignableFrom(type.GetGenericTypeDefinition()))
                return type;

            Type[] ifaces = type.GetInterfaces();
            if (ifaces != null)
                for (int i = 0; i < ifaces.Length; i++)
                {
                    Type current = GetGenericServerRequest(ifaces[i]);
                    if (current != null)
                        return current;
                }

            return null;
        }
    }
}

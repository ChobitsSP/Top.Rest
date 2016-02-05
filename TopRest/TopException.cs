using System;
using System.Runtime.Serialization;

namespace Top.Rest
{
    /// <summary>
    /// TOP客户端异常。
    /// </summary>
    public class TopException : Exception
    {
        private int errorCode;
        private string errorMsg;

        public TopException()
            : base()
        {
        }

        public TopException(string message)
            : base(message)
        {
        }

        protected TopException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public TopException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public TopException(int errorCode, string errorMsg)
            : base(errorCode + ":" + errorMsg)
        {
            this.errorCode = errorCode;
            this.errorMsg = errorMsg;
        }

        public int ErrorCode
        {
            get { return this.errorCode; }
        }

        public string ErrorMsg
        {
            get { return this.errorMsg; }
        }
    }
}

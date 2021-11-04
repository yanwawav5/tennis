using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Common
{
    public class schoolException: Exception
    {
        private int errorCode;
        private string errorMsg;

        public schoolException()
            : base()
        {
        }

        public schoolException(string message)
            : base(message)
        {
        }
        public schoolException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public schoolException(int errorCode, string errorMsg = "")
            : base(errorMsg)
        {
            this.errorCode = errorCode;
            this.errorMsg = errorMsg;
        }

        public int ErrorCode
        {
            get { return this.errorCode; }
        }
    }
}

using System;

namespace Cmt.Bll.Services.Exceptions
{
    public class CmtException : Exception
    {
        public CmtException() {}

        public CmtException(string errorCode)
        {
            Error = new ErrorResult(errorCode);
        }

        public ErrorResult Error { get; set; }
    }
}

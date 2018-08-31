using System;
using System.Collections.Generic;

namespace Cmt.Bll.Services.Exceptions
{
    public class CmtException : Exception
    {
        public ErrorResult Error { get; set; }
    }
}

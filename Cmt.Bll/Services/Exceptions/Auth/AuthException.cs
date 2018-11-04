using System.Collections.Generic;

namespace Cmt.Bll.Services.Exceptions.Auth
{
    public class AuthException: CmtException
    {
        public IEnumerable<ErrorResult> Errors { get; set; }
    }
}

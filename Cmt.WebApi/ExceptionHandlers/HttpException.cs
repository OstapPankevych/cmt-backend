using System.Collections.Generic;

namespace Cmt.WebApi.ExceptionHandlers
{
    public class HttpException
    {
        public int StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}

using System.Collections.Generic;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers
{
    public class HttpException
    {
        public int StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}

using System.Collections.Generic;

namespace Cmt.WebApi.Infrastructure.ExceptionHandlers
{
    public class HttpError
    {
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
    }
}

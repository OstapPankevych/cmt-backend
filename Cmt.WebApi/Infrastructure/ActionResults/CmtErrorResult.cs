using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cmt.WebApi.Infrastructure.Extensions;
using System.Collections.Generic;

namespace Cmt.WebApi.ActionResults.Infrastructure
{
    public class CmtErrorResult: IActionResult
    {
        private readonly int _statusCode;
        private readonly IList<string> _errors;
        private readonly string _message;

        public CmtErrorResult(
            int statusCode,
            string error = null,
            string message = null)
        {
            _statusCode = statusCode;
            _errors = error !=  null 
                ? new List<string> { error }
                : new List<string>();
            _message = message;
        }

        public CmtErrorResult(
            int statusCode, 
            IList<string> errors, 
            string message = null)
          : this(statusCode)
        {
            _errors = errors;
            _message = message;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return context.HttpContext.WriteErrorAsync(_statusCode, _errors, _message);
        }
    }
}

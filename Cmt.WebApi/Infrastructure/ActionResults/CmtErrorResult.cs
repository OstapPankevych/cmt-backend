using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cmt.WebApi.Infrastructure.Extensions;
using Cmt.WebApi.Infrastructure.HttpErrors;

namespace Cmt.WebApi.ActionResults.Infrastructure
{
    public class CmtErrorResult: IActionResult
    {
        private readonly HttpError _httpError;

        public CmtErrorResult(HttpError httpError)
        {
            _httpError = httpError;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return context.HttpContext.WriteErrorAsync(_httpError);
        }
    }
}

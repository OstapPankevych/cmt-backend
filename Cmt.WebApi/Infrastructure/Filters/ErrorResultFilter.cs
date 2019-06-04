using System.Linq;
using System.Threading.Tasks;
using Cmt.WebApi.ActionResults.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cmt.WebApi.Infrastructure.Filters
{
  public class ErrorResultFilter : IAsyncResultFilter
  {
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
      var obj = context.Result as ObjectResult;
      switch (obj?.Value)
      {
        case ValidationProblemDetails validationProblemDetails:
          context.Result = HandleValidationProblem(validationProblemDetails);
          break;
        case ProblemDetails problemDetails:
          context.Result = HandleProblem(problemDetails);
          break;
      }

      await next();
    }

    private CmtErrorResult HandleProblem(ProblemDetails problemDetails)
    {
      var statusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
      return new CmtErrorResult(statusCode, problemDetails.Title, problemDetails.Detail);
    }

    private CmtErrorResult HandleValidationProblem(ValidationProblemDetails validationDetails)
    {
      var errorCodes = validationDetails.Errors
        .Select(e => e.Key)
        .ToList();

      var message = string.Empty;
      foreach (var (key, value) in validationDetails.Errors)
      {
        var msg = string.Join(",", value.Select(x => x));
        message += $"{key}:{msg}; ";
      }

      return new CmtErrorResult(StatusCodes.Status400BadRequest, errorCodes, message);
    }
  }
}

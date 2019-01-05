using System;
using System.Security.Claims;
using Cmt.Bll.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiErrors = Cmt.WebApi.Infrastructure.ExceptionHandlers;

namespace Cmt.WebApi.Controllers
{
    public class CmtController: Controller
    {
        public IActionResult Created(object obj)
        {
            return StatusCode(StatusCodes.Status201Created, obj);
        }

        public DateTime? GetLastModifiedUtcHeader()
        {
            return Request.GetTypedHeaders().LastModified.Value.UtcDateTime;
        }

        public string GetCurrentUserClaim(string claim)
        {
            return User
                .FindFirst(x => x.Type == claim).Value;
        }

        public int GetCurrentUserId()
        {
            var idString = GetCurrentUserClaim(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idString, out var id))
            {
                throw new CmtException(WebApiErrors.ErrorCodes.NameIdentifierClaimMissed);
            }

            return id;
        }
    }
}

using System;
using System.Globalization;
using System.Security.Claims;
using Cmt.Bll.Services.Exceptions;
using Cmt.WebApi.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cmt.WebApi.Controllers
{
    public class CmtController: Controller
    {
        protected IActionResult Created<T>(T obj)
        {
            return StatusCode(StatusCodes.Status201Created, obj);
        }

        protected DateTime? GetLastModifiedUtcHeader()
        {
            var lastModifiedHeader = Request.Headers["Last-Modified"];
            if (DateTime.TryParse(lastModifiedHeader, 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.AssumeUniversal, out var dateTime))
            {
                return dateTime;
            }

            return null;
        }

        protected int GetCurrentUserId()
        {
            var idString = GetCurrentUserClaim(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idString, out var id))
            {
                throw new CmtException(WebApiErrors.NameIdentifierClaimMissed);
            }

            return id;
        }

        private string GetCurrentUserClaim(string claim)
        {
            return User.FindFirst(x => x.Type == claim).Value;
        }
    }
}

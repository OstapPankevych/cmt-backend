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

        protected DateTime GetRequireLastModifierUtcHeader()
        {
            var lastModifier = GetLastModifiedUtcHeader();

            return lastModifier != null
                ? lastModifier.Value
                : throw new CmtException(CmtErrorCodes.LastModified);
        }

        protected int? GetCurrentUserId()
        {
            var idString = GetCurrentUserClaim(ClaimTypes.NameIdentifier);

            if (int.TryParse(idString, out var id))
            {
                return id;
            }

            return null;
        }

        protected int GetRequireCurrentUserId()
        {
            var userId = GetCurrentUserId();

            return userId != null
                ? userId.Value
                : throw new CmtException(WebApiErrors.NameIdentifierClaimMissed);
        }

        private string GetCurrentUserClaim(string claim)
        {
            return User.FindFirst(x => x.Type == claim).Value;
        }
    }
}

using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Cmt.Bll.Services.Exceptions;
using Cmt.WebApi.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cmt.WebApi.Controllers
{
    public class CmtController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        protected CmtController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        protected IActionResult Created<T>(T obj)
        {
            return StatusCode(StatusCodes.Status201Created, obj);
        }

        protected DateTime? GetLastModifiedUtcHeader()
        {
            var lastModifiedHeader = Request.Headers[Headers.LastModified];
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

            return lastModifier ?? throw new CmtException(CmtErrorCodes.LastModified);
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

            return userId ?? throw new CmtException(WebApiErrors.NameIdentifierClaimMissed);
        }

        private string GetCurrentUserClaim(string claim)
        {
            return User.FindFirst(x => x.Type == claim).Value;
        }


        protected async Task<bool> IsAuthorizeed(string policy, object resource)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, resource, policy);

            return isAuthorized.Succeeded;
        }
    }
}

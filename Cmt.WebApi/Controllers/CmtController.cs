using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using Cmt.Bll.Services.Exceptions;
using Cmt.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cmt.WebApi.Infrastructure.HttpErrors;

namespace Cmt.WebApi.Controllers
{
    public class CmtController: Controller
    {
        protected IActionResult Created(object obj)
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

        protected string GetCurrentUserClaim(string claim)
        {
            return User
                .FindFirst(x => x.Type == claim).Value;
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

        protected SimpleResponse<TModel> CreateResponse<TModel>(TModel model)
            => new SimpleResponse<TModel> { Data = model };

        protected ArrayResponse<TModel> CreateResponse<TModel>(IList<TModel> models)
            => new ArrayResponse<TModel> { Data = models };
    }
}

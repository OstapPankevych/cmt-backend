using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Cmt.WebApi.Infrastructure.Attributes
{
    public class JwtAuthorizeAttribute: AuthorizeAttribute
    {
        public new string AuthenticationSchemes => base.AuthenticationSchemes;

        public JwtAuthorizeAttribute()
            : this(null)
        { }

        public JwtAuthorizeAttribute(string policy)
            : base(policy)
        {
            base.AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}

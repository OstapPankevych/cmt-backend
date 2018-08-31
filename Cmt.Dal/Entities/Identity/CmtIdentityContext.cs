using System;
using Cmt.Common.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cmt.Dal.Entities.Identity
{
    public class CmtIdentityContext : IdentityDbContext<CmtIdentityUser, CmtIdentityRole, int>
    {
        public CmtIdentityContext(DbContextOptions<CmtIdentityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

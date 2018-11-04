﻿using Cmt.Dal.Entities.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cmt.Dal.Ef
{
    public class CmtIdentityContext
        : IdentityDbContext<CmtIdentityUser, CmtIdentityRole, int>
    {
        public CmtIdentityContext(DbContextOptions<CmtIdentityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
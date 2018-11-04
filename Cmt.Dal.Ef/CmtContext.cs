﻿using Cmt.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cmt.Dal.Ef
{
    public class CmtContext: DbContext
    {
        public CmtContext(DbContextOptions<CmtContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CourseEntity> Courses { get; set; }
    }
}
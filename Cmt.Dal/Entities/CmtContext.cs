using System;
using Microsoft.EntityFrameworkCore;
namespace Cmt.Dal.Entities
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

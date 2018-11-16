using Cmt.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cmt.Dal.Ef
{
    public class CmtContext: DbContext
    {
        public CmtContext(DbContextOptions<CmtContext> options)
            : base(options)
        {
        }

        public DbSet<CourseEntity> Courses { get; set; }
    }
}

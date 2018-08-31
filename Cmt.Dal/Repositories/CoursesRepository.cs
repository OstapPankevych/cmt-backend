using System;
using System.Threading.Tasks;
using Cmt.Dal.Entities;
using Cmt.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cmt.Dal.Repositories
{
    public class CoursesRepository : Repository<CourseEntity>, ICoursesRepository
    {
        public CoursesRepository(DbContextOptions<CmtContext> options)
            : base(options)
        {}
    }
}

using Cmt.Dal.Entities;
using Cmt.Dal.Repositories.Interfaces;

namespace Cmt.Dal.Repositories
{
    public class CoursesRepository : Repository<CourseEntity, int>, ICoursesRepository
    {
        public CoursesRepository(CmtContext dbContext)
            : base(dbContext)
        {}
    }
}

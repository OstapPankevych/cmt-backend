using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces.Repositories;

namespace Cmt.Dal.Ef.Repositories
{
    public class CoursesRepository : Repository<CourseEntity, int>, ICoursesRepository
    {
        public CoursesRepository(CmtContext dbContext)
            : base(dbContext)
        {}
    }
}

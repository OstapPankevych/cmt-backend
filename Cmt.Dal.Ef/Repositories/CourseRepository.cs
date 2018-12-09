using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces.Repositories;

namespace Cmt.Dal.Ef.Repositories
{
    public class CourseRepository : Repository<CourseEntity, int>, ICourseRepository
    {
        public CourseRepository(CmtContext dbContext)
            : base(dbContext)
        {}
    }
}

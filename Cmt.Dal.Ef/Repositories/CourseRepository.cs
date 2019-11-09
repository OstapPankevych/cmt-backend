using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces.Repositories;

namespace Cmt.Dal.Ef.Repositories
{
    public class CourseRepository : Repository<CourseEntity>, ICourseRepository
    {
        public CourseRepository(CmtContext dbContext)
            : base(dbContext)
        {}
    }
}

using System;
namespace Cmt.Dal.Entities
{
    public class CourseEntity: Entity<int>
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}

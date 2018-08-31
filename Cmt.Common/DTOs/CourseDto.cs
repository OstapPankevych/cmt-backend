using System;
namespace Cmt.Common.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public CourseType Type { get; set; }
    }
}

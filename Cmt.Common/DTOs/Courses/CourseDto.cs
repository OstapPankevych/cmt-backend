using System;
namespace Cmt.Common.DTOs.Courses
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public CourseTypeDto Type { get; set; }
    }
}

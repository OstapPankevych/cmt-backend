using System;

namespace Cmt.Bll.DTOs.Courses
{
    public class CourseDto: Dto<int>
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}

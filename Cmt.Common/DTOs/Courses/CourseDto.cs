using System;

namespace Cmt.Common.DTOs.Courses
{
    public class CourseDto: BaseDto<int>
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public CourseTypeDto Type { get; set; }
    }
}

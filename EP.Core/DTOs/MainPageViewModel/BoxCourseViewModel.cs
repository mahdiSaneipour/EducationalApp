using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.MainPageViewModel
{
    public class BoxCourseViewModel
    {
        public int CourseId { get; set; }

        public string CourseTitle { get; set; }

        public int CoursePrice { get; set; }

        public TimeSpan CourseTime { get; set; }

        public string CourseImage { get; set; }
    }
}

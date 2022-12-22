using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.AdminPanelViewModels
{
    public class CourseViewModel
    {
        [Display(Name = "ایدی دوره")]
        public int UserId { get; set; }

        [Display(Name = "نام دوره")]
        public string CourseName { get; set; }

        [Display(Name = "تصویر دوره")]
        public string CourseImage { get; set; }

        [Display(Name = "تعداد جلسات")]
        public int Count { get; set; }
    }
}

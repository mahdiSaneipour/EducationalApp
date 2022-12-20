using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Course
{
    public class CourseGroup
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "نام گروه")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]    
        public string GroupeName { get; set; }

        [Display(Name = "آیا حذف شده")]
        public bool IsDelete { get; set; }

        [Display(Name = "زیر مجموعه")]
        public int? ParentId { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public List<CourseGroup> Parent { get; set; }

        [InverseProperty("CourseGroup")]
        public List<Course> CoursesGroupes { get; set; }

        [InverseProperty("CourseSubGroup")]
        public List<Course> CoursesSubGroupes { get; set; }

        #endregion
    }
}

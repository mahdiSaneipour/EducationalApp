using EP.Domain.Entities.Order;
using EP.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Course
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Display(Name = "نام دوره")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string CourseName { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public string CourseDescription { get; set; }

        [Display(Name = "قسمت دوره")]
        public int CoursePrice { get; set; }

        [Display(Name = "تصویر دوره")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string CourseImage { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Tags { get; set; }

        [Display(Name = "دمو دوره")]
        public string? CourseDemo { get; set; }

        [Display(Name = "تاریخ ایجاد دوره")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ اخرین آپدیت دوره")]
        public DateTime UpdateDate { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public int SubGroupId { get; set; }

        [Required]
        public int LevelId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        #region

        [ForeignKey("GroupId")]
        public CourseGroup? CourseGroup { get; set; }

        [ForeignKey("SubGroupId")]
        public CourseGroup? CourseSubGroup { get; set; }

        [ForeignKey("LevelId")]
        public CourseLevel? CourseLevel { get; set; }

        [ForeignKey("StatusId")]
        public CourseStatus? CourseStatus { get; set; }

        [ForeignKey("TeacherId")]
        public User.User? User { get; set; }

        public List<CourseEpisode>? Episodes { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        public List<UserCourses> UserCourses { get; set; }

        public List<CourseComment> CourseComments { get; set; }

        #endregion
    }
}

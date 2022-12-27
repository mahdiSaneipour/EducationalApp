using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Course
{
    public class CourseEpisode
    {
        [Key]
        public int EpisodeId { get; set; }

        [Display(Name = "نام قسمت")] 
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string EpisodeTitle { get; set; }

        [Display(Name = "زمان دوره")]
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public TimeSpan EpisodeTime { get; set; }

        [Display(Name = "فایل دمو")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string EpisodeDemoFile { get; set; }

        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }

        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public int CourseId { get; set; }

        #region

        public Course Course { get; set; }

        #endregion
    }
}

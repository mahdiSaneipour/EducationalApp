using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Question
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Display(Name = "سرتیتر")]     
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        [MaxLength(200,ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "متن")]      
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public string Body { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        #region Relation

        public List<Answer> Answers { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; }

        [ForeignKey("CourseId")]
        public Course.Course Course { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Question
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Display(Name = "متن پاسخ")]      
        [Required(ErrorMessage = "فیلد {0} نمیتواند خالی باشد")]
        public string BodyAnswer { get; set; }

        public DateTime CreateDate { get; set; }

        #region

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; }

        [InverseProperty("Answer")]
        public Question AnswerQuestion { get; set; }

        #endregion
    }
}

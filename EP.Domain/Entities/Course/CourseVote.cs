using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.Course
{
    public class CourseVote
    {
        [Key]
        public int VoteId { get; set; }

        public bool VoteValue { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int UserId { get; set; }

        #region Relations

        public Course Course { get; set; }

        public User.User User { get; set; }

        #endregion
    }
}

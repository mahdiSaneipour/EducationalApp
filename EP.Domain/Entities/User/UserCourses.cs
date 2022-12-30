using EP.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Entities.User
{
    public class UserCourses
    {
        [Key]
        public int UC_Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CourseId { get; set; }

        #region Relations

        public User User { get; set; }

        public Course.Course Course { get; set; }

        #endregion
    }
}

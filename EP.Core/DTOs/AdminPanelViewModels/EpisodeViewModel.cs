using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.DTOs.AdminPanelViewModels
{
    public class EpisodeViewModel
    {
        public int CourseId { get; set; }

        public string TeacherName { get; set; }

        public string CourseName { get; set; }

        public List<Domain.Entities.Course.CourseEpisode> Episodes { get; set; }
    }
}

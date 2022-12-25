using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.ServiceModels.Course
{
    public class EditCourseServiceModel
    {
        public Domain.Entities.Course.Course Course { get; set; }

        public SelectList Groups { get; set; }
        public SelectList SubGroups { get; set; }
        public SelectList Statuses { get; set; }
        public SelectList Levels { get; set; }
        public SelectList Teachers { get; set; }
    }
}

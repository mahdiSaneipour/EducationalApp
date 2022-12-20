using EP.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.Course
{
    public interface ICourseServices
    {
        public List<CourseGroup> GetAllCourseGroups();
    }
}

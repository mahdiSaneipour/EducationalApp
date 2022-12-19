using EP.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Interfaces.Course
{
    public interface ICourseRepository
    {
        public List<CourseGroupe> GetAllCourseGroups();
    }
}

using EP.Core.Interfaces.Course;
using EP.Domain.Entities.Course;
using EP.Domain.Interfaces.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Services.Course
{
    public class CourseServices : ICourseServices
    {
        private readonly ICourseRepository _courseRepository;

        public CourseServices(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public List<CourseGroup> GetAllCourseGroups()
        {
            return _courseRepository.GetAllCourseGroups();
        }
    }
}

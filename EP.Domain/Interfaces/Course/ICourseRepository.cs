using EP.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace EP.Domain.Interfaces.Course
{
    public interface ICourseRepository
    {
        public List<CourseGroup> GetAllCourseGroups();

        public List<CourseGroup> GetAllMainCourseGroups();

        public List<CourseGroup> GetCourseGroupsByParentId(int parentId);

        public List<CourseLevel> GetAllCourseLevels();

        public List<CourseStatus> GetAllCourseStatuses();

        public int AddCourse(Domain.Entities.Course.Course course);

        public IQueryable<Domain.Entities.Course.Course> GetAllCoursesIQ();

        public void SaveChanges();
    }
}

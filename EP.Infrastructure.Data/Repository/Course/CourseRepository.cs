using EP.Domain.CustomModel.Episode;
using EP.Domain.Entities.Course;
using EP.Domain.Interfaces.Course;
using EP.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Infrastructure.Data.Repository.Course
{
    public class CourseRepository : ICourseRepository
    {
        private readonly EPContext _context;

        public CourseRepository(EPContext context)
        {
            _context = context;
        }


        public List<CourseGroup> GetAllCourseGroups()
        {
            return _context.CourseGroupes.ToList();
        }

        public List<CourseGroup> GetAllMainCourseGroups()
        {
            return _context.CourseGroupes.Where(g => g.ParentId == null).ToList();
        }

        public List<CourseGroup> GetCourseGroupsByParentId(int parentId)
        {
            return _context.CourseGroupes.Where(g => g.ParentId == parentId).ToList();
        }

        public List<CourseLevel> GetAllCourseLevels()
        {
            return _context.Levels.ToList();
        }

        public List<CourseStatus> GetAllCourseStatuses()
        {
            return _context.Statuses.ToList();
        }

        public int AddCourse(Domain.Entities.Course.Course course)
        {
            _context.Courses.Add(course);
            return course.CourseId;
        }

        public IQueryable<Domain.Entities.Course.Course> GetAllCoursesIQ()
        {
            return _context.Courses;
        }


        public Domain.Entities.Course.Course GetCourseById(int courseId)
        {
            return _context.Courses.Include(c => c.User).First(c => c.CourseId == courseId);
        }

        public void UpdateCourse(Domain.Entities.Course.Course course)
        {
            _context.Courses.Update(course);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void DeleteCourse(Domain.Entities.Course.Course course)
        {
            _context.Courses.Remove(course);
        }
    }
}

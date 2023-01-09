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

        public void DeleteCourse(Domain.Entities.Course.Course course)
        {
            _context.Courses.Remove(course);
        }

        public bool IsCourseExist(int courseId)
        {
            return _context.Courses.Any(c => c.CourseId == courseId);
        }

        public IEnumerable<Domain.Entities.Course.Course> GetAllCourses()
        {
            return _context.Courses.Include(c => c.Episodes);
        }

        public Domain.Entities.Course.Course GetCourseByCourseIdForShowCourse(int courseId)
        {
            return _context.Courses.Include(c => c.Episodes).Include(c => c.CourseStatus)
                .Include(c => c.CourseLevel).Include(c => c.User)
                .Include(c => c.OrderDetails)
                .FirstOrDefault(c => c.CourseId == courseId);
        }

        public int GetCoursePriceByCourseId(int courseId)
        {
            return _context.Courses.FirstOrDefault(c => c.CourseId == courseId).CoursePrice;
        }

        public IEnumerable<CourseComment> GetCourseCommentsByCourseId(int courseId)
        {
            return _context.CourseComments.Include(cc => cc.User).Where(c => c.CourseId == courseId)
                .OrderByDescending(cc => cc.CreateDate);
        }

        public void AddCourseComment(CourseComment courseComment)
        {
            _context.CourseComments.Add(courseComment);
        }

        public IEnumerable<Domain.Entities.Course.Course> GetPopularCourses()
        {
            return _context.Courses.Include(c => c.Episodes).Where(c => c.OrderDetails.Any())
                .OrderByDescending(c => c.OrderDetails.Count).Take(8);
        }

        public CourseGroup GetCourseGroupByGroupId(int groupId)
        {
            return _context.CourseGroupes.FirstOrDefault(cg => cg.GroupId == groupId);
        }

        public void DeleteCourseGroup(CourseGroup courseGroup)
        {
            _context.CourseGroupes.Remove(courseGroup);
        }

        public void AddCourseGroup(CourseGroup courseGroup)
        {
            _context.CourseGroupes.Add(courseGroup);
        }

        public void UpdateCourseGroup(CourseGroup courseGroup)
        {
            _context.CourseGroupes.Update(courseGroup);
        }

        public void AddCourseVote(CourseVote courseVote)
        {
            _context.CourseVotes.Add(courseVote);
        }

        public CourseVote GetCourseVoteByUserIdAndCourseId(int userId, int courseId)
        {
            return _context.CourseVotes.FirstOrDefault(cv => cv.UserId == userId && cv.CourseId == courseId);
        }

        public void UpdateCourseVote(CourseVote courseVote)
        {
            _context.CourseVotes.Update(courseVote);
        }

        public Tuple<int, int> GetNumberOfVotes(int courseId)
        {
            var votes = _context.CourseVotes.Where(cv => cv.CourseId == courseId).Select(cv => cv.VoteValue);

            return Tuple.Create(votes.Count(cv => cv), votes.Count(cv => !cv));
        }

        public bool IsCourseFree(int courseId)
        {
            return _context.Courses.Any(c => c.CourseId == courseId && c.CoursePrice == 0);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

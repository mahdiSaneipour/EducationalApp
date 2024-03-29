﻿using EP.Domain.CustomModel.Episode;
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
        public Domain.Entities.Course.Course GetCourseByCourseIdForShowCourse(int courseId);

        public Domain.Entities.Course.CourseGroup GetCourseGroupByGroupId(int groupId);

        public IEnumerable<Entities.Course.Course> GetAllCourses();

        public List<CourseGroup> GetAllCourseGroups();

        public List<CourseGroup> GetAllMainCourseGroups();

        public List<CourseGroup> GetCourseGroupsByParentId(int parentId);

        public List<CourseLevel> GetAllCourseLevels();

        public List<string> GetCourseNameAsSearch(string filter);

        public IEnumerable<Domain.Entities.Course.Course> GetPopularCourses();

        public List<CourseStatus> GetAllCourseStatuses();

        public int GetCoursePriceByCourseId(int courseId);

        public Domain.Entities.Course.CourseVote GetCourseVoteByUserIdAndCourseId(int userId, int courseId);

        public Tuple<int, int> GetNumberOfVotes(int courseId);

        public int AddCourse(Domain.Entities.Course.Course course);

        public IQueryable<Domain.Entities.Course.Course> GetAllCoursesIQ();

        public Domain.Entities.Course.Course GetCourseById(int courseId);

        public void UpdateCourse(Domain.Entities.Course.Course course);

        public void UpdateCourseGroup(CourseGroup courseGroup);

        public void UpdateCourseVote(CourseVote courseVote);

        public void DeleteCourse(Domain.Entities.Course.Course course);

        public void DeleteCourseGroup(Domain.Entities.Course.CourseGroup courseGroup);

        public bool IsCourseExist(int courseId);

        public bool IsCourseFree(int courseId);

        public IEnumerable<CourseComment> GetCourseCommentsByCourseId(int courseId);

        public void AddCourseComment(CourseComment courseComment);

        public void AddCourseGroup(CourseGroup courseGroup);

        public void AddCourseVote(CourseVote courseVote);

        public void SaveChanges();
    }
}

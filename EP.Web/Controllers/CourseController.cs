﻿using EP.Core.DTOs.MainPageViewModel;
using EP.Core.Enums.Course;
using EP.Core.Interfaces.Course;
using EP.Core.Interfaces.Order;
using EP.Domain.Entities.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EP.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseServices _courseServices;
        private readonly IOrderServices _orderServices;

        public CourseController(ICourseServices courseServices, IOrderServices orderServices)
        {
            _courseServices = courseServices;
            _orderServices = orderServices;
        }

        public IActionResult Index(int pageId = 1,
            BoxCourseOrderByEnum orderBy = BoxCourseOrderByEnum.CreateDate,
            BoxCourseGetTypeEnum getType = BoxCourseGetTypeEnum.All, int minimumPrice = 0,
            int maximumPrice = 0, string filter = "", List<int> courseGroups = null)
        {

            Tuple<List<BoxCourseViewModel>, int> result = _courseServices.GetAllCourseByFilter(pageId,orderBy,getType,minimumPrice, maximumPrice, filter,courseGroups,9);

            ViewData["CourseGroups"] = _courseServices.GetAllCourseGroups();
            ViewData["SCourseGroups"] = courseGroups;
            ViewData["MinimumPrice"] = minimumPrice;
            ViewData["MaximumPrice"] = maximumPrice;
            ViewData["Filter"] = filter;
            ViewData["OrderBy"] = orderBy;
            ViewData["GetType"] = getType;
            ViewData["PageId"] = pageId;

            return View(result);
        }

        [Route("ShowCourse/{courseId}")]
        public IActionResult ShowCourse(int courseId)
        {
            Domain.Entities.Course.Course course = _courseServices.GetCourseByCourseIdForShowCourse(courseId);

            return View(course);
        }

        public IActionResult BuyCourse(int courseId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            int orderId = _orderServices.AddOrder(userId,courseId);

            return Redirect("~/UserPanel/Order/ShowOrder/" + orderId);
        }

        public IActionResult DownloadEpisode(int episodeId, int courseId)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (_courseServices.IsUserAccessToEpisode(userId, episodeId, courseId))
            {
                Tuple<string, string> result = _courseServices.DownloadCourseFile(episodeId);

                byte[] file = System.IO.File.ReadAllBytes(result.Item1);
                return File(file, "application/force-download", result.Item2);
            }

            return Forbid();
        }

        [HttpPost]
        public IActionResult AddCourseComment(CourseComment comment)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            comment.UserId = userId;

            _courseServices.AddCourseComment(comment);

            return RedirectToAction("ShowComments", new{ courseId = comment.CourseId });
        }

        [Route("Course/ShowComments/{courseId}/{pageId?}")]
        public IActionResult ShowComments(int courseId, int? pageId = 1)
        {
            return View(_courseServices.GetCourseCommentsByCourseId(courseId, (int)pageId));
        }

        [Route("Course/VoteCourse/{courseId}")]
        public IActionResult VoteCourse(int courseId)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (_courseServices.IsAccessToVote(userId, courseId))
            {
                return PartialView("Partial Views/_VoteCoursePartialView", _courseServices.GetNumberOfVotes(courseId));
            } else
            {
                return null;
            }
        }

        [Route("Course/AddVote/{courseId}/{vote}")]
        public IActionResult AddVote(int courseId, bool vote)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            _courseServices.AddCourseVote(userId, courseId, vote);

            return PartialView("Partial Views/_VoteCoursePartialView", _courseServices.GetNumberOfVotes(courseId));
        }
    }
}
using EP.Core.DTOs.ForumViewModel;
using EP.Core.Interfaces.Forum;
using EP.Domain.Entities.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EP.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly IUserForumServices _userForumServices;

        public ForumController(IUserForumServices userForumServices)
        {
            _userForumServices = userForumServices;
        }

        [Route("Forum/{courseId?}")]
        public IActionResult Index(int? courseId = 0, int? pageId = 1)
        {
            Tuple<List<Question>, int> result = _userForumServices.GetQuestions(courseId, pageId);

            ViewBag.CurrentPage = pageId;
            ViewBag.CourseId = courseId;

            return View(result);
        }

        [Authorize]
        [Route("Forum/CreateQuestion/{courseId}")]
        public IActionResult CreateQuestion(int courseId)
        {
            Question question = new Question()
            {
                CourseId = courseId
            };

            return View(question);
        }

        [HttpPost]
        [Authorize]
        [Route("Forum/CreateQuestion/{courseId}")]
        public IActionResult CreateQuestion(Question question)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            question.UserId = userId;

            _userForumServices.AddQuestion(question);

            return RedirectToAction("ShowQuestion", new { questionId = question.QuestionId });
        }

        [Route("Forum/ShowQuestion/{questionId}")]
        public IActionResult ShowQuestion(int questionId)
        {
            ShowForumViewModel forum = _userForumServices.GetQuestionAndAnswersByQuestionId(questionId);

            if (forum == null)
            {
                return NotFound();
            }

            return View(forum);
        }

        public IActionResult AddAnswer(int questionId, string answerBody)
        {

            if (!String.IsNullOrEmpty(answerBody))
            {
                int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                _userForumServices.AddAnswer(userId, questionId, answerBody);
            }

            return RedirectToAction("ShowQuestion", new { questionId = questionId});
        }

        public IActionResult SetAnswerForQuestion(int answerId, int questionId)
        {
            int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _userForumServices.SetAnswerForQuestion(userId, answerId, questionId);

            return RedirectToAction("ShowQuestion", new { questionId = questionId });
        }
    }
}

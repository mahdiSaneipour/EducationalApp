using EP.Core.DTOs.MainPageViewModel;
using EP.Core.Interfaces.Course;
using EP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseServices _courseServices;

        public HomeController(ICourseServices courseServices)
        {
            _courseServices = courseServices;

        }        
        public IActionResult Index()
        {
            Tuple<List<BoxCourseViewModel>, int> result = _courseServices.GetAllCourseByFilter();

            ViewData["PopularCourses"] = _courseServices.GetPopularCourses();

            return View(result);
        }
    }
}
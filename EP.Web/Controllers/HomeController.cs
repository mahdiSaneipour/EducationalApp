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
            List<BoxCourseViewModel> result = _courseServices.GetAllCourseByFilter().Item1;

            ViewData["PopularCourses"] = _courseServices.GetPopularCourses();

            return View(result);
        }
    }
}
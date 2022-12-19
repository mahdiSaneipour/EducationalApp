using EP.Core.Interfaces.Admin;
using EP.Core.Interfaces.Course;
using EP.Domain.Entities.Course;
using Microsoft.AspNetCore.Mvc;

namespace EP.Web.Components.Course
{
    public class CourseGroupComponent:ViewComponent
    {
        private readonly ICourseServices _courseServices;

        public CourseGroupComponent(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<CourseGroupe> courseGroupes = _courseServices.GetAllCourseGroups();

            return await Task.FromResult((IViewComponentResult) View("../Courses/CourseGroupComponent/CourseGroup",courseGroupes));
        }

    }
}

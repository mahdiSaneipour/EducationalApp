using EP.Domain.Interfaces.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EP.Web.Api.Course
{
    [ApiController]
    public class ApiCourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public ApiCourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        [Route("Api/Course/Search")]
        [Produces("application/json")]
        public async Task<IActionResult> SearchCourse()
        {
            try
            {
                string filter = HttpContext.Request.Query["term"].ToString();
                List<string> result = _courseRepository.GetCourseNameAsSearch(filter);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

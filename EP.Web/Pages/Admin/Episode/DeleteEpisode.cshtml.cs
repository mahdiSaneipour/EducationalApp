using EP.Core.Interfaces.Episode;
using EP.Core.Tools.Security;
using EP.Domain.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Episode
{
    [PermissionChecker(19)]
    public class DeleteEpisodeModel : PageModel
    {
        private readonly IEpisodeServices _episodeService;

        public DeleteEpisodeModel(IEpisodeServices episodeService)
        {
            _episodeService = episodeService;
        }

        [BindProperty]
        public Domain.Entities.Course.CourseEpisode Episode { get; set; }

        public void OnGet(int episodeId)
        {
            Episode = _episodeService.GetEpisodeByEpisodeId(episodeId);
        }

        public IActionResult OnPost()
        {
            int courseId = Episode.CourseId;
            _episodeService.DeleteEpisode(Episode);

            return RedirectToPage("Index", new { courseId = courseId });
        }
    }
}

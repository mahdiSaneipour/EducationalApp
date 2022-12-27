using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Episode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EP.Web.Pages.Admin.Episode
{
    public class IndexModel : PageModel
    {
        private readonly IEpisodeServices _episodeServices;

        public IndexModel(IEpisodeServices episodeServices)
        {
            _episodeServices = episodeServices;
        }

        public EpisodeViewModel courseEpisodes { get; set; }

        public void OnGet(int courseId)
        {
            courseEpisodes = _episodeServices.GetEpisodesForAdminPanelByCourseId(courseId);
        }
    }
}

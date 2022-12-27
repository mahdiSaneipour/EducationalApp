using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Episode;
using EP.Domain.CustomModel.Episode;
using EP.Domain.Interfaces.Episode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Services.Episode
{
    public class EpisodeServices : IEpisodeServices
    {
        private readonly IEpisodeRepository _episodeRepository;


        public EpisodeServices(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        public EpisodeViewModel GetEpisodesForAdminPanelByCourseId(int courseId)
        {
            EpisodeViewModel result = new EpisodeViewModel();

            EpisodeDetailsCustomModel details = _episodeRepository.GetEpisodeDetailsByCourseId(courseId);
            List<Domain.Entities.Course.CourseEpisode> episodes =
                _episodeRepository.GetAllEpisodesByCourseId(courseId);

            result.Episodes = episodes;
            result.CourseName = details.CourseName;
            result.TeacherName = details.TeacherName;

            return result;
        }
    }
}

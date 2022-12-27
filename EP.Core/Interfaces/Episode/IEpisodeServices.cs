using EP.Core.DTOs.AdminPanelViewModels;
using EP.Domain.Entities.Course;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.Episode
{
    public interface IEpisodeServices
    {
        #region GET

        public EpisodeViewModel GetEpisodesForAdminPanelByCourseId(int courseId);

        public Domain.Entities.Course.CourseEpisode GetEpisodeByEpisodeId(int episodeId);

        #endregion

        #region Create

        public int CreateEpisode(int courseId, IFormFile episodeFile, Domain.Entities.Course.CourseEpisode episode);

        #endregion

        #region Edit

        public void EditEpisode(IFormFile episodeFile, CourseEpisode course);

        #endregion

        #region Delete

        public void DeleteEpisode(Domain.Entities.Course.CourseEpisode course);

        #endregion
    }
}

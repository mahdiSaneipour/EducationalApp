using EP.Domain.CustomModel.Episode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Interfaces.Episode
{
    public interface IEpisodeRepository
    {
        #region GET

        public List<Domain.Entities.Course.CourseEpisode> GetAllEpisodesByCourseId(int courseId);

        public EpisodeDetailsCustomModel GetEpisodeDetailsByCourseId(int courseId);

        #endregion

        #region ADD

        public void AddEpisode(Domain.Entities.Course.CourseEpisode episode);

        #endregion

        #region Update

        public void UpdateEpisode(Domain.Entities.Course.CourseEpisode episode);

        #endregion

        #region Remove

        public void RemoveEpisode(Domain.Entities.Course.CourseEpisode episode);

        #endregion
    }
}

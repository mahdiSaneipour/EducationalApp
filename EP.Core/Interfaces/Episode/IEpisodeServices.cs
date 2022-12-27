using EP.Core.DTOs.AdminPanelViewModels;
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

        #endregion

        #region Create



        #endregion

        #region Edit
        #endregion

        #region Delete
        #endregion
    }
}

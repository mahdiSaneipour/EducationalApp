using EP.Domain.CustomModel.Episode;
using EP.Domain.Entities.Course;
using EP.Domain.Interfaces.Episode;
using EP.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Infrastructure.Data.Repository.Episode
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly EPContext _context;

        public EpisodeRepository(EPContext context)
        {
            _context= context;
        }

        #region Add

        public void AddEpisode(CourseEpisode episode)
        {
            _context.Episodes.Add(episode);
        }

        #endregion

        #region Get

        public List<CourseEpisode> GetAllEpisodesByCourseId(int courseId)
        {
            return _context.Episodes.Where(e => e.CourseId == courseId).ToList();
        }

        public EpisodeDetailsCustomModel GetEpisodeDetailsByCourseId(int courseId)
        {
            return _context.Courses.Where(c => c.CourseId == courseId).Include(c => c.User).Select(c => new EpisodeDetailsCustomModel()
            {
                TeacherName = c.User.UserName,
                CourseName = c.CourseName,
                CourseId = c.CourseId
            }).FirstOrDefault();
        }


        public CourseEpisode GetEpisodeByEpisodeId(int episodeId)
        {
            return _context.Episodes.FirstOrDefault(e => e.EpisodeId == episodeId);
        }


        public bool IsEpisodeFree(int episodeId)
        {
            return _context.Episodes.Any(e => e.EpisodeId == episodeId && e.IsFree == true);
        }

        #endregion

        #region Remove

        public void RemoveEpisode(Domain.Entities.Course.CourseEpisode episode)
        {
            _context.Episodes.Remove(episode);
        }

        #endregion

        #region Update

        public void UpdateEpisode(CourseEpisode episode)
        {
            _context.Episodes.Update(episode);
        }

        #endregion


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

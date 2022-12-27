using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Episode;
using EP.Domain.CustomModel.Episode;
using EP.Domain.Entities.Course;
using EP.Domain.Interfaces.Episode;
using Microsoft.AspNetCore.Http;
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

        #region GET

        public Domain.Entities.Course.CourseEpisode GetEpisodeByEpisodeId(int episodeId)
        {
            return _episodeRepository.GetEpisodeByEpisodeId(episodeId);
        }

        public EpisodeViewModel GetEpisodesForAdminPanelByCourseId(int courseId)
        {
            EpisodeViewModel result = new EpisodeViewModel();

            EpisodeDetailsCustomModel details = _episodeRepository.GetEpisodeDetailsByCourseId(courseId);
            List<Domain.Entities.Course.CourseEpisode> episodes = _episodeRepository.GetAllEpisodesByCourseId(courseId);

            result.Episodes = episodes;
            result.CourseId = details.CourseId;
            result.CourseName = details.CourseName;
            result.TeacherName = details.TeacherName;

            return result;
        }

        #endregion

        #region Create 

        public int CreateEpisode(int courseId, IFormFile episodeFile, CourseEpisode episode)
        {
            string episodeAddress = DeletePreviousImageUploadeNewImage
                (episode.EpisodeDemoFile,episodeFile,"wwwroot/videos/episode");

            episode.EpisodeDemoFile = episodeAddress;
            episode.CourseId = courseId;

            _episodeRepository.AddEpisode(episode);
            _episodeRepository.SaveChanges();

            return episode.EpisodeId;
        }

        #endregion

        #region Update

        public void EditEpisode(IFormFile episodeFile, CourseEpisode course)
        {
            string episodeAddress = DeletePreviousImageUploadeNewImage(course.EpisodeDemoFile, episodeFile, "wwwroot/videos/episode");
            course.EpisodeDemoFile = episodeAddress;

            _episodeRepository.UpdateEpisode(course);
            _episodeRepository.SaveChanges();
        }

        #endregion

        #region Remove

        public void DeleteEpisode(CourseEpisode course)
        {
            _episodeRepository.RemoveEpisode(course);
            _episodeRepository.SaveChanges();
        }

        #endregion

        private string DeletePreviousImageUploadeNewImage(string previousImage, IFormFile newImage, string address)
        {
            string result = "";

            if (!string.IsNullOrEmpty(previousImage))
            {
                string coursePreviousDemoPath = Path.Combine(Directory.GetCurrentDirectory(), address, previousImage);

                if (File.Exists(coursePreviousDemoPath))
                {
                    File.Delete(coursePreviousDemoPath);
                }

            }

            if (newImage != null)
            {
                string courseDemoName = Tools.Generator.NameGenerator.GenerateUniqCode() + Path.GetExtension(newImage.FileName);

                string courseImagePath = Path.Combine(Directory.GetCurrentDirectory(), address, courseDemoName);

                using (var stream = new FileStream(courseImagePath, FileMode.Create))
                {
                    newImage.CopyTo(stream);
                }

                result = courseDemoName;
            }

            return result;

        }
    }
}

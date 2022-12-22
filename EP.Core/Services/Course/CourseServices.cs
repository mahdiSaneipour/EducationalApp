using EP.Core.Enums.UserPanel;
using EP.Core.Interfaces.Course;
using EP.Core.JsonModel.UserPanel;
using EP.Core.ServiceModels.UserPanel;
using EP.Domain.Entities.Course;
using EP.Domain.Interfaces.Course;
using EP.Domain.Interfaces.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EP.Core.Services.Course
{
    public class CourseServices : ICourseServices
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public CourseServices(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public List<CourseGroup> GetAllCourseGroups()
        {
            return _courseRepository.GetAllCourseGroups();
        }

        public SelectList GetAllCourseLevelsAsSelectList()
        {
            List<CourseLevel> courseLevels = _courseRepository.GetAllCourseLevels();
            SelectList result = null;
            List<SelectListItem> levels = new List<SelectListItem>();

            foreach (var level in courseLevels)
            {
                levels.Add(new SelectListItem()
                {
                    Text = level.LevelName,
                    Value = level.LevelId.ToString()
                });
            }

            result = new SelectList(levels, "Value", "Text");

            return result;
        }

        public SelectList GetAllCourseStatusesAsSelectList()
        {
            List<CourseStatus> courseStatuses = _courseRepository.GetAllCourseStatuses();
            SelectList result = null;
            List<SelectListItem> statuses = new List<SelectListItem>();

            foreach (var status in courseStatuses)
            {
                statuses.Add(new SelectListItem()
                {
                    Text = status.StatusName,
                    Value = status.StatusId.ToString()
                });
            }

            result = new SelectList(statuses, "Value", "Text");

            return result;
        }

        public SelectList GetAllMainCourseGroupsAsSelectList()
        {
            List<CourseGroup> courseGroups = _courseRepository.GetAllMainCourseGroups();
            SelectList result = null;
            List<SelectListItem> groups = new List<SelectListItem>();

            foreach (var group in courseGroups)
            {
                groups.Add(new SelectListItem()
                {
                    Text = group.GroupeName,
                    Value = group.GroupId.ToString()
                });
            }

            result = new SelectList(groups, "Value", "Text");

            return result;
        }

        public SelectList GetCourseGroupsByParentIdAsSelectList(int parentId)
        {
            List<CourseGroup> courseSubGroups = _courseRepository.GetCourseGroupsByParentId(parentId);
            SelectList result = null;
            List<SelectListItem> subGroups = new List<SelectListItem>();

            foreach (var group in courseSubGroups)
            {
                subGroups.Add(new SelectListItem()
                {
                    Text = group.GroupeName,
                    Value = group.GroupId.ToString()
                });
            }

            result = new SelectList(subGroups, "Value", "Text");

            return result;
        }

        public SelectList GetAllTeachersAsSelectList()
        {
            List<Domain.Entities.User.User> UTeachers = _userRepository.GetAllTeachers();
            SelectList result = null;
            List<SelectListItem> teachers = new List<SelectListItem>();

            foreach (var teacher in UTeachers)
            {
                teachers.Add(new SelectListItem()
                {
                    Text = teacher.UserName,
                    Value = teacher.UserId.ToString()
                });
            }

            result = new SelectList(teachers, "Value", "Text");

            return result;
        }

        public ChangeAvatarServiceModel UploadImageCourseAndDeletePreviousOne(IFormFile newCourseImage, string courseImage)
        {

            try
            {
                if (newCourseImage != null)
                {
                    Console.WriteLine("perImage : " + courseImage);
                    if (courseImage != "NoPreviousImage")
                    {
                        string previousAvatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course-images/normal-size/", courseImage);

                        if (File.Exists(previousAvatarPath))
                        {
                            File.Delete(previousAvatarPath);
                        }
                        else
                        {
                            return SetChangeAvatarServiceModel(ChangeAvatarEnums.PreviousAvatarNotFound, ""); ;
                        }

                    }

                    string courseImageName = Tools.Generator.NameGenerator.GenerateUniqCode() +
                        newCourseImage.ContentType.Replace("image/", ".");

                    string courseImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course-images/normal-size/", courseImageName);

                    using (var stream = new FileStream(courseImagePath, FileMode.Create))
                    {
                        newCourseImage.CopyTo(stream);
                    }

                    return SetChangeAvatarServiceModel(ChangeAvatarEnums.Successful, courseImageName); ;

                }
                else
                {
                    return SetChangeAvatarServiceModel(ChangeAvatarEnums.AvatarFileIsNull, ""); ;
                }
            }
            catch
            {
                return SetChangeAvatarServiceModel(ChangeAvatarEnums.ServerError, ""); ;
            }


        }

        public ChangeAvatarServiceModel SetChangeAvatarServiceModel(ChangeAvatarEnums status, string avatarAddress)
        {
            ChangeAvatarServiceModel result = new ChangeAvatarServiceModel();

            result.AvatarAddress = avatarAddress;
            result.Status = status;

            return result;
        }

        public int AddCourse(Domain.Entities.Course.Course course)
        {
            course.CreateDate = DateTime.Now;
            course.CourseDemo = "null";

            int courseId = _courseRepository.AddCourse(course);
            _courseRepository.SaveChanges();
            return courseId;
        }
    }
}

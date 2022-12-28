using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.DTOs.MainPageViewModel;
using EP.Core.Enums.Course;
using EP.Core.Enums.UserPanel;
using EP.Core.Interfaces.Course;
using EP.Core.JsonModel.UserPanel;
using EP.Core.ServiceModels.Course;
using EP.Core.ServiceModels.UserPanel;
using EP.Domain.Entities.Course;
using EP.Domain.Interfaces.Course;
using EP.Domain.Interfaces.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public SelectList GetAllCourseLevelsAsSelectList(int? selected = 0)
        {
            List<CourseLevel> courseLevels = _courseRepository.GetAllCourseLevels();
            SelectList result = null;
            List<SelectListItem> levels = new List<SelectListItem>();

            foreach (var level in courseLevels)
            {
                levels.Add(new SelectListItem()
                {
                    Text = level.LevelName,
                    Value = level.LevelId.ToString(),
                });
            }

            result = new SelectList(levels, "Value", "Text", selected);

            return result;
        }

        public SelectList GetAllCourseStatusesAsSelectList(int? selected = 0)
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

            result = new SelectList(statuses, "Value", "Text", selected);

            return result;
        }

        public SelectList GetAllMainCourseGroupsAsSelectList(int? selected = 0)
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

            result = new SelectList(groups, "Value", "Text", selected);

            return result;
        }

        public SelectList GetCourseGroupsByParentIdAsSelectList(int parentId, int? selected = 0)
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

            result = new SelectList(subGroups, "Value", "Text", selected);

            return result;
        }

        public SelectList GetAllTeachersAsSelectList(int? selected = 0)
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

            result = new SelectList(teachers, "Value", "Text", selected);

            return result;
        }

        public ChangeAvatarServiceModel UploadImageCourseAndDeletePreviousOne(IFormFile newCourseImage, string courseImage)
        {

            try
            {
                if (newCourseImage != null)
                {
                    if (courseImage != "Default.jpg")
                    {
                        string previousImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course-images/normal-size/", courseImage);
                        string previousThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course-images/thumb-size/", courseImage);

                        if (File.Exists(previousImagePath))
                        {
                            File.Delete(previousImagePath);

                            if (File.Exists(previousThumbPath))
                            {
                                File.Delete(previousThumbPath);
                            }
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

                    string thumbImageCoursePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course-images/thumb-size/", courseImageName);

                    var imageResizer = new Tools.ImageResizer.ImageConvertor();
                    imageResizer.Image_resize(courseImagePath, thumbImageCoursePath, 100);

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

        public int AddCourse(Domain.Entities.Course.Course course, IFormFile courseDemo)
        {
            course.CreateDate = DateTime.Now;

            if (!string.IsNullOrEmpty(course.CourseDemo)) {
                string courseDemoName = Tools.Generator.NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);

                string courseImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos/course", courseDemoName);

                using (var stream = new FileStream(courseImagePath, FileMode.Create))
                {
                    courseDemo.CopyTo(stream);
                }

                course.CourseDemo = courseDemoName;
            }

            int courseId = _courseRepository.AddCourse(course);
            _courseRepository.SaveChanges();
            return courseId;
        }

        public List<CourseViewModel> GetAllCoursesForAdmin()
        {
            List<CourseViewModel> result = new List<CourseViewModel>();

            IQueryable<Domain.Entities.Course.Course> Courses = _courseRepository.GetAllCoursesIQ();

            result.AddRange(Courses.Select(c => new CourseViewModel()
            {
                Count = 0,
                CourseImage = c.CourseImage,
                CourseName = c.CourseName,
                CourseId = c.CourseId
            }).ToList());

            return result;
        }

        public string UploadImageCourseForDescription(IFormFile descriptionImage)
        {
            string descriptionImageName = Tools.Generator.NameGenerator.GenerateUniqCode() + Path.GetExtension(descriptionImage.FileName);

            string descriptionImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course-images/description/", descriptionImageName);

            using (var stream = new FileStream(descriptionImagePath, FileMode.Create))
            {
                descriptionImage.CopyTo(stream);
            }

            return descriptionImageName;
        }

        public EditCourseServiceModel GetCourseInformationForEdit(int courseId)
        {
            Domain.Entities.Course.Course course = _courseRepository.GetCourseById(courseId);

            SelectList group = GetAllMainCourseGroupsAsSelectList(course.GroupId);
            SelectList subGroup = GetCourseGroupsByParentIdAsSelectList(int.Parse(group.First().Value),course.SubGroupId);
            SelectList statuses = GetAllCourseStatusesAsSelectList(course.StatusId);
            SelectList levels = GetAllCourseLevelsAsSelectList(course.LevelId);
            SelectList teachers = GetAllTeachersAsSelectList(course.TeacherId);

            EditCourseServiceModel result = new EditCourseServiceModel()
            {
                Course = course,
                Groups = group,
                SubGroups = subGroup,
                Statuses = statuses,
                Levels = levels,
                Teachers = teachers
            };

            return result;
        }

        public bool EditCourse(Domain.Entities.Course.Course course, IFormFile courseDemo)
        {
            try {

                if (!string.IsNullOrEmpty(course.CourseDemo))
                {
                    string coursePreviousDemoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos/course", course.CourseDemo);

                    if (File.Exists(coursePreviousDemoPath))
                    {
                        File.Delete(coursePreviousDemoPath);
                    }

                }

                if (courseDemo != null)
                {
                    string courseDemoName = Tools.Generator.NameGenerator.GenerateUniqCode() + Path.GetExtension(courseDemo.FileName);

                    string courseImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos/course", courseDemoName);

                    using (var stream = new FileStream(courseImagePath, FileMode.Create))
                    {
                        courseDemo.CopyTo(stream);
                    }

                    course.CourseDemo = courseDemoName;
                }

                course.UpdateDate = DateTime.Now;

                _courseRepository.UpdateCourse(course);
                _courseRepository.SaveChanges();

                return true;
            } catch {
                return false;
            }
        }

        public Domain.Entities.Course.Course GetCourseByCourseId(int courseId)
        {
            return _courseRepository.GetCourseById(courseId);
        }

        public bool DeleteCourse(Domain.Entities.Course.Course course)
        {
            try
            {
                _courseRepository.DeleteCourse(course);
                _courseRepository.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsCourseExist(int courseId)
        {
            return _courseRepository.IsCourseExist(courseId);
        }

        public List<BoxCourseViewModel> GetAllCourseByFilter(int pageId = 1,
            BoxCourseOrderByEnum orderBy = BoxCourseOrderByEnum.CreateDate,
            BoxCourseGetTypeEnum getType = BoxCourseGetTypeEnum.All, int minimumPrice = 0, 
            int maximumPrce = 0, string filter = "", List<int> courseGroups = null,
            int take = 0)
        {

            if (take == 0)
            {
                take = 8;
            }

            IEnumerable<Domain.Entities.Course.Course> courses = _courseRepository.GetAllCourses();
            
            switch (getType)
            {
                case BoxCourseGetTypeEnum.All:
                    break;

                case BoxCourseGetTypeEnum.Buyable:
                    {
                        courses = courses.Where(c => c.CoursePrice != 0);
                        break;
                    }

                case BoxCourseGetTypeEnum.Free:
                    {
                        courses = courses.Where(c => c.CoursePrice == 0);
                        break;
                    }
            }

            switch (orderBy)
            {
                case BoxCourseOrderByEnum.CreateDate:
                    courses = courses.OrderBy(c => c.CreateDate);
                    break;

                case BoxCourseOrderByEnum.UpdateDate:
                    {
                        courses = courses.OrderBy(c => c.UpdateDate);
                        break;
                    }

                case BoxCourseOrderByEnum.Cheaper:
                    {
                        courses = courses.OrderByDescending(c => c.CoursePrice);
                        break;
                    }

                case BoxCourseOrderByEnum.MostExpensive:
                    {
                        courses = courses.OrderBy(c => c.CoursePrice);
                        break;
                    }
            }

            if (!String.IsNullOrEmpty(filter)) {
                courses = courses.Where(c => c.CourseName.Contains(filter));
            }

            if (minimumPrice != 0)
            {
                courses = courses.Where(c => c.CoursePrice > minimumPrice);
            }


            if (maximumPrce != 0)
            {
                courses = courses.Where(c => c.CoursePrice < maximumPrce);
            }

            if(courseGroups != null)
            {
                // filter buy course
            }
            Console.WriteLine("courseId : " + courses.First().CourseId);
            int skip = (pageId - 1) * take;

            return courses.Select(c =>
            
                new BoxCourseViewModel() {
                    CourseId = c.CourseId,
                    CourseImage = c.CourseImage,
                    CoursePrice = c.CoursePrice,
                    CourseTime = new TimeSpan((long)c.Episodes.Sum(e => e.EpisodeTime.Ticks)),
                    CourseTitle = c.CourseName
                }
            ).Skip(skip).Take(take).ToList();
        }
    }
}
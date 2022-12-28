using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.DTOs.MainPageViewModel;
using EP.Core.Enums.Course;
using EP.Core.ServiceModels.Course;
using EP.Core.ServiceModels.UserPanel;
using EP.Domain.Entities.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.Course
{
    public interface ICourseServices
    {
        public List<CourseGroup> GetAllCourseGroups();

        public SelectList GetAllMainCourseGroupsAsSelectList(int? selected = 0);

        public SelectList GetCourseGroupsByParentIdAsSelectList(int parentId, int? selected = 0);

        public SelectList GetAllCourseLevelsAsSelectList(int? selected = 0);

        public SelectList GetAllCourseStatusesAsSelectList(int? selected = 0);

        public SelectList GetAllTeachersAsSelectList(int? selected = 0);

        public List<CourseViewModel> GetAllCoursesForAdmin();

        public Domain.Entities.Course.Course GetCourseByCourseId(int courseId);

        public List<BoxCourseViewModel> GetAllCourseByFilter(int pageId = 1,
            BoxCourseOrderByEnum orderBy = BoxCourseOrderByEnum.CreateDate,
            BoxCourseGetTypeEnum getType = BoxCourseGetTypeEnum.All, int minimumPrice = 0,
            int maximumPrce = 0, string filter = "", List<int> courseGroups = null,
            int take = 0);

        public int AddCourse(Domain.Entities.Course.Course course, IFormFile courseDemo);

        public ChangeAvatarServiceModel UploadImageCourseAndDeletePreviousOne(IFormFile newCourseImage, string courseImage);

        public EditCourseServiceModel GetCourseInformationForEdit(int courseId);

        public string UploadImageCourseForDescription(IFormFile descriptionImage);

        public bool EditCourse(Domain.Entities.Course.Course course, IFormFile courseDemo);

        public bool DeleteCourse(Domain.Entities.Course.Course course);

        public bool IsCourseExist(int courseId);
    }
}

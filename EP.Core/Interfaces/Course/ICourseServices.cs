using EP.Core.DTOs.AdminPanelViewModels;
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

        public int AddCourse(Domain.Entities.Course.Course course, IFormFile courseDemo);

        public ChangeAvatarServiceModel UploadImageCourseAndDeletePreviousOne(IFormFile newCourseImage, string courseImage);

        public EditCourseServiceModel GetCourseInformationForEdit(int courseId);

        public string UploadImageCourseForDescription(IFormFile descriptionImage);

        public bool EditCourse(Domain.Entities.Course.Course course, IFormFile courseDemo);

        public bool DeleteCourse(Domain.Entities.Course.Course course);
    }
}

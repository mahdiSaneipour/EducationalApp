using EP.Core.DTOs.AdminPanelViewModels;
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

        public SelectList GetAllMainCourseGroupsAsSelectList();

        public SelectList GetCourseGroupsByParentIdAsSelectList(int parentId);

        public SelectList GetAllCourseLevelsAsSelectList();

        public SelectList GetAllCourseStatusesAsSelectList();

        public SelectList GetAllTeachersAsSelectList();

        public List<CourseViewModel> GetAllCoursesForAdmin();

        public int AddCourse(Domain.Entities.Course.Course course, IFormFile courseDemo);

        public ChangeAvatarServiceModel UploadImageCourseAndDeletePreviousOne(IFormFile newCourseImage, string courseImage);

        public string UploadImageCourseForDescription(IFormFile descriptionImage);
    }
}

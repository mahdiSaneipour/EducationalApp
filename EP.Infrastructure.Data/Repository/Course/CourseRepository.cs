using EP.Domain.Entities.Course;
using EP.Domain.Interfaces.Course;
using EP.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Infrastructure.Data.Repository.Course
{
    public class CourseRepository : ICourseRepository
    {
        private readonly EPContext _context;

        public CourseRepository(EPContext context)
        {
            _context = context;
        }

        public List<CourseGroup> GetAllCourseGroups()
        {
            return _context.CourseGroupes.ToList();
        }
    }
}

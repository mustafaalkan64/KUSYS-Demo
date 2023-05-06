using KUSYS_Demo.Core.Interfaces;
using KUSYS_Demo.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Infastructure.Repositories
{
    public class StudentCourseRepository : GenericRepository<StudentCourses>, IStudentCourseRepository
    {
        public StudentCourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task AddMultipleStudentCourses(int studentId, int[] courseIds)
        {
            if(studentId > 0)
            {
                var studentCourses = _dbContext.StudentCourses.Where(x => x.StudentId == studentId).ToList();
                _dbContext.StudentCourses.RemoveRange(studentCourses);
                
                foreach (var courseId in courseIds)
                {
                    var studentCourse = new StudentCourses()
                    {
                        StudentId = studentId,
                        CourseId = courseId
                    };
                    await _dbContext.StudentCourses.AddAsync(studentCourse);
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<StudentCourses>> GetCoursesByStudentId(int studentId)
        {
            return await _dbContext.StudentCourses.Include(x => x.Course).Include(x => x.Student).Where(x => x.StudentId == studentId).ToListAsync();
        }

        public async Task<IEnumerable<StudentCourses>> GetAllStudentCoursesAsync()
        {
            return await _dbContext.StudentCourses.Include(x => x.Course).Include(x => x.Student).ToListAsync();
        }

        public async Task RemoveStudentCoursesByStudentIdAsync(int studentId)
        {
            var studentCourses = _dbContext.StudentCourses.Where(x => x.StudentId == studentId);
            if(studentCourses != null)
            {
                _dbContext.StudentCourses.RemoveRange(studentCourses);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}

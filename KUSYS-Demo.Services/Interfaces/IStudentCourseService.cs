using KUSYS_Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Services.Interfaces
{
    public interface IStudentCourseService
    {
        Task AddMultipleStudentCourses(int studentId, int[] courseIds);
        Task<IEnumerable<StudentCourses>> GetCoursesByStudentId(int studentId);
        Task<IEnumerable<StudentCourses>> GetAllStudentCoursesAsync();
        Task RemoveStudentCoursesByStudentIdAsync(int studentId);
    }
}

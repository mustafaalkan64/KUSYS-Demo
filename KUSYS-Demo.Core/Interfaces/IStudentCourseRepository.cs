using KUSYS_Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Core.Interfaces
{
    public interface IStudentCourseRepository : IGenericRepository<StudentCourses>
    {
        Task AddMultipleStudentCourses(int studentId, int[] courseIds);
        Task<IEnumerable<StudentCourses>> GetCoursesByStudentId(int studentId);
        Task<IEnumerable<StudentCourses>> GetAllStudentCoursesAsync();
        Task RemoveStudentCoursesByStudentIdAsync(int studentId);
    }
}

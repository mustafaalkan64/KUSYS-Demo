using KUSYS_Demo.Core.Interfaces;
using KUSYS_Demo.Core.Models;
using KUSYS_Demo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Services
{
    public class StudentCourseService: IStudentCourseService
    {
        public IUnitOfWork _unitOfWork;

        public StudentCourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Assign Multiple Courses to Students
        /// </summary>
        public async Task AddMultipleStudentCourses(int studentId, int[] courseIds)
        {
            await _unitOfWork.StudentCourse.AddMultipleStudentCourses(studentId, courseIds);
        }

        public async Task<IEnumerable<StudentCourses>> GetCoursesByStudentId(int studentId)
        {
            return await _unitOfWork.StudentCourse.GetCoursesByStudentId(studentId);
        }

        public async Task<IEnumerable<StudentCourses>> GetAllStudentCoursesAsync()
        {
            return await _unitOfWork.StudentCourse.GetAllStudentCoursesAsync();
        }
  
        public async Task RemoveStudentCoursesByStudentIdAsync(int studentId)
        {
            await _unitOfWork.StudentCourse.RemoveStudentCoursesByStudentIdAsync(studentId);
        }



    }
}

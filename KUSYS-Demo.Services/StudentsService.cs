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
    public class StudentService : IStudentService
    {
        public IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Student student)
        {
            if (student != null)
            {
                await _unitOfWork.Student.AddAsync(student);

                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task RemoveAsync(int studentId)
        {
            if (studentId > 0)
            {
                var student = await _unitOfWork.Student.GetByIdAsync(studentId);
                if (student != null)
                {
                    _unitOfWork.Student.Remove(student);
                    await _unitOfWork.CompleteAsync();
                }
            }
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _unitOfWork.Student.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int studentId)
        {
            if (studentId > 0)
            {
                var student = await _unitOfWork.Student.GetByIdAsync(studentId);
                if (student != null) return student;
            }
            return null;
        }

        public async Task UpdateAsync(Student studentModel)
        {
            if (studentModel != null)
            {
                var student = await _unitOfWork.Student.GetByIdAsync(studentModel.StudentId);
                if (student != null)
                {
                    student.FirstName = studentModel.FirstName;
                    student.LastName = studentModel.LastName;
                    student.Birthday = studentModel.Birthday;

                    _unitOfWork.Student.Update(student);

                    await _unitOfWork.CompleteAsync();
                }
            }
        }
    }
}

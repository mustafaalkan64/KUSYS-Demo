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
    public class StudentsService : IStudentsService
    {
        public IUnitOfWork _unitOfWork;

        public StudentsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(Students student)
        {
            if (student != null)
            {
                await _unitOfWork.Students.Add(student);

                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task Delete(int studentId)
        {
            if (studentId > 0)
            {
                var student = await _unitOfWork.Students.GetById(studentId);
                if (student != null)
                {
                    _unitOfWork.Students.Delete(student);
                    await _unitOfWork.CompleteAsync();

                }
            }
        }

        public async Task<IEnumerable<Students>> GetAll()
        {
            return await _unitOfWork.Students.GetAll();
        }

        public async Task<Students> GetProductById(int studentId)
        {
            if (studentId > 0)
            {
                var student = await _unitOfWork.Students.GetById(studentId);
                if (student != null) return student;
            }
            return null;
        }

        public async Task Update(Students studentModel)
        {
            if (studentModel != null)
            {
                var student = await _unitOfWork.Students.GetById(studentModel.StudentId);
                if (student != null)
                {
                    student.FirstName = studentModel.FirstName;
                    student.LastName = studentModel.LastName;
                    student.Birthday = studentModel.Birthday;

                    _unitOfWork.Students.Update(student);

                    await _unitOfWork.CompleteAsync();
                }
            }
        }
    }
}

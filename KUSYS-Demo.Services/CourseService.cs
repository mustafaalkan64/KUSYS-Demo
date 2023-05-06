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
    public class CourseService: ICourseService
    {
        public IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _unitOfWork.Course.GetAllAsync();
        }
    }
}

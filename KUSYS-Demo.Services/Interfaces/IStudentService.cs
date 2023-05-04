using KUSYS_Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Services.Interfaces
{
    public interface IStudentService
    {
        Task Create(Student student);

        Task<IEnumerable<Student>> GetAll();

        Task<Student> GetProductById(int productId);

        Task Update(Student student);

        Task Delete(int studentId);
    }
}

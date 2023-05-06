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
        Task CreateAsync(Student student);

        Task<IEnumerable<Student>> GetAllAsync();

        Task<Student> GetByIdAsync(int productId);

        Task UpdateAsync(Student student);

        Task RemoveAsync(int studentId);
    }
}

using KUSYS_Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Services.Interfaces
{
    public interface IStudentsService
    {
        Task Create(Students student);

        Task<IEnumerable<Students>> GetAll();

        Task<Students> GetProductById(int productId);

        Task Update(Students student);

        Task Delete(int studentId);
    }
}

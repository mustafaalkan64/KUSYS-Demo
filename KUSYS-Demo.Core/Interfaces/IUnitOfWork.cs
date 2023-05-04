using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Student { get; }
        ICourseRepository Course { get; }
        Task CompleteAsync();
    }
}

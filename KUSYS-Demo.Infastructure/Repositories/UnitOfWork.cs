using KUSYS_Demo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Infastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IStudentRepository Students { get; }
        public ICourseRepository Courses { get; }

        public UnitOfWork(ApplicationDbContext dbContext,
                            IStudentRepository StudentsRepository,
                            ICourseRepository CoursesRepository)
        {
            _dbContext = dbContext;
            Students = StudentsRepository;
            Courses = CoursesRepository;
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}

using KUSYS_Demo.Core.Interfaces;
using KUSYS_Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Infastructure.Repositories
{
    public class CoursesRepository : GenericRepository<Courses>, ICourseRepository
    {
        public CoursesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}

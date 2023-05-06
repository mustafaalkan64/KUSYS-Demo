using KUSYS_Demo.Core.Interfaces;
using KUSYS_Demo.Infastructure;
using KUSYS_Demo.Infastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.UI.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IStudentRepository, StudentsRepository>();
            services.AddTransient<ICourseRepository, CoursesRepository>();
            services.AddTransient<IStudentCourseRepository, StudentCourseRepository>();

            return services;
        }
    }
}

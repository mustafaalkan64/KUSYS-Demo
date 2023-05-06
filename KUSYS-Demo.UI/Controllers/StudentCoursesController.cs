using KUSYS_Demo.Core.Models;
using KUSYS_Demo.Core.ViewModels;
using KUSYS_Demo.Services.Interfaces;
using KUSYS_Demo.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Diagnostics;

namespace KUSYS_Demo.UI.Controllers
{
    public class StudentCoursesController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly IStudentCourseService _studentCourseService;

        public StudentCoursesController(ILogger<HomeController> logger, ICourseService courseService, IStudentService studentService, IStudentCourseService studentCourseService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager) : base(userManager, signInManager, roleManager)
        {
            _logger = logger;
            _courseService = courseService;
            _studentCourseService = studentCourseService;
            _studentService = studentService;
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Index(int id)
        {
            if (User.IsInRole("User"))
            {
                AppUser currentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
                var studentId = currentUser.StudentId;
                if(id != studentId) {
                    return RedirectToAction("AccessDenied", "Account"); }
            }

            var studentCourses = await _studentCourseService.GetCoursesByStudentId(id);
            var courses = await _courseService.GetAllAsync();
            var courseViewModel = courses.Select(a => new CourseViewModel()
            {
                CourseId = a.CourseId,
                CourseName = a.CourseName,
                Id = a.Id,
                Checked = studentCourses.Any(x => x.CourseId == a.Id)
            });
            var studentCourseViewModel = new StudentCourseViewModel()
            {
                StudentId = id,
                Courses = courseViewModel
            };
            return View(studentCourseViewModel);


        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Matches(int id)
        {
            IEnumerable<StudentCourses> result = null;
            if (User.IsInRole("User"))
            {
                AppUser currentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
                var studentId = currentUser.StudentId;
                result = await _studentCourseService.GetCoursesByStudentId(studentId.Value); // Get Student Courses By Student Id
            }
            else
            {
                result = await _studentCourseService.GetAllStudentCoursesAsync(); // Get All Student Courses
            }
            return View(result);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<JsonResult> SaveStudentCourses(SaveStudentCourseViewModel saveStudentCourseViewModel)
        {
            var student = await _studentService.GetByIdAsync(saveStudentCourseViewModel.StudentId);
            if(saveStudentCourseViewModel.CourseIds == null || saveStudentCourseViewModel.CourseIds.Length == 0)
            {
                return Json(new { Status = false, Message = "En Az 1 Kurs Seçmelisiniz" });
            }
            if (student == null)
            {
                return Json(new { Status = false, Message = "Öğrenci Bulunamadı!" });
            }
            else {
                // Assign Multiple Courses to StudentId
                await _studentCourseService.AddMultipleStudentCourses(saveStudentCourseViewModel.StudentId, saveStudentCourseViewModel.CourseIds);
                return Json(new { Status = true, Message = "Öğrenci Kursları Başarıyla Kaydedildi" });
            }
        }
    }
}
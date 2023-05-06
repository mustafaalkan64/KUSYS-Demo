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
    public class StudentsController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IStudentService _studentService;
        private readonly IStudentCourseService _studentCourseService;

        public StudentsController(ILogger<HomeController> logger, IStudentService studentService, IStudentCourseService studentCourseService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager) : base(userManager, signInManager, roleManager)
        {
            _logger = logger;
            _studentService = studentService;
            _studentCourseService = studentCourseService;
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Index()
        {
            // TODO: Escape from Magic String and move to Consts
            if(User.IsInRole("Admin"))
            {
                var result = await _studentService.GetAllAsync();
                return View(result);
            }
            else
            {
                // If user is in User Role, list only himself/herself information
                AppUser currentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
                var studentId = currentUser.StudentId;
                var student = await _studentService.GetByIdAsync(studentId.Value);
                var result = new List<Student>() {  student };
                return View(result);
            }

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Title = "Yeni Öğrenci Ekleme";
            return View(new StudentViewModel());
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<JsonResult> GetById(int id)
        {
            var student = await _studentService.GetByIdAsync(id); // Get Student By Id
            var user = userManager.Users.FirstOrDefault(x => x.StudentId == id); // Get User By Student Id
            StudentViewModel studentViewModel = new StudentViewModel()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthdayStr = student.Birthday.Value.ToString("dd.MM.yyyy"),
                Email = user.Email,
                UserName = user.UserName,
                StudentId = id,
            };
            return Json(studentViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var student = _studentService.GetByIdAsync(id).Result; // Check If Student Exists
            if (student != null)
            {
                var user = userManager.Users.FirstOrDefault(x => x.StudentId == id); // Check If User Exists
                if (user != null)
                {
                    StudentViewModel studentViewModel = new StudentViewModel()
                    {
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Birthday = student.Birthday,
                        Email = user.Email,
                        UserName = user.UserName,
                        StudentId = id,
                    };
                    ViewBag.Title = "Öğrenci Düzenleme";
                    return View("Create", studentViewModel);
                }
                else
                {
                    ViewBag.Error = "User Not Found";
                    return View("Create", new StudentViewModel());
                }
            }
            else
            {
                ViewBag.Error = "Student Not Found";
                return View("Create", new StudentViewModel());
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> CreateOrUpdate(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                if(studentViewModel.StudentId == 0)
                {
                    var student = new Student()
                    {
                        Birthday = studentViewModel.Birthday,
                        FirstName = studentViewModel.FirstName,
                        LastName = studentViewModel.LastName,
                    };
                    await _studentService.CreateAsync(student);

                    AppUser user = new AppUser();
                    user.UserName = studentViewModel.UserName;
                    user.Email = studentViewModel.Email;
                    user.StudentId = student.StudentId;
                    user.NormalizedEmail = studentViewModel.Email.ToUpper();
                    user.NormalizedUserName = studentViewModel.UserName.ToUpper();

                    // Check any email exists with same email address
                    if (userManager.Users.Any(u => u.Email == studentViewModel.Email))
                    {
                        return Json(new { Message = "Email Adres Zaten Kayıtlı!", Status = false });
                    }

                    // Create a new Application User
                    IdentityResult result = await userManager.CreateAsync(user, "Password_123");

                    if (result.Succeeded)
                    {
                        // Add User Role to User
                        await userManager.AddToRoleAsync(user, "User");
                        return Json(new { Message = "Yeni Öğrenci Başarılı ile Kaydedildi", Status = true });
                    }
                    else
                    {
                        var errorList = result.Errors.Select(x => x.Description);
                        return Json(new { Message = string.Join(",", errorList), Status = false });
                    }
                }
                else
                {
                    var student = _studentService.GetByIdAsync(studentViewModel.StudentId).Result;
                    if(student != null) {
                        student.FirstName = studentViewModel.FirstName;
                        student.LastName = studentViewModel.LastName;
                        student.Birthday = studentViewModel.Birthday;
                        await _studentService.UpdateAsync(student);

                        var user = userManager.Users.FirstOrDefault(x => x.StudentId == studentViewModel.StudentId);
                        if(user != null)
                        {
                            user.UserName = studentViewModel.UserName;
                            user.Email = studentViewModel.Email;
                            // Apply the changes if any to the db
                            await userManager.UpdateAsync(user);
                        }
                        return Json(new { Message = "Öğrenci Başarıyla Güncellendi", Status = true });

                    }
                    else
                    {
                        return Json(new { Message = "Öğrenci Bulunamadı!", Status = false });
                    }
                }
                
            }
            else
            {
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
                return Json(new { Message = string.Join(",", errorList), Status = false });
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<JsonResult> Delete(int studentId)
        {
            var student = _studentService.GetByIdAsync(studentId).Result;
            if (student != null)
            {
                // Remove Student Courses
                await _studentCourseService.RemoveStudentCoursesByStudentIdAsync(studentId);

                // Remove Student
                await _studentService.RemoveAsync(studentId);
                var user = userManager.Users.Where(x => x.StudentId == studentId).FirstOrDefault();
                if (user != null)
                {
                    await userManager.RemoveFromRoleAsync(user, "User"); // Remove User's roles
                    await userManager.DeleteAsync(user); // Remove Application User
                }
                return Json(new { Status = true, Message = "Öğrenci Başarıyla Silindi!" });
            }
            else { return Json(new { Status = false, Message = "Öğrenci Bulunamadı!" }); }

        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
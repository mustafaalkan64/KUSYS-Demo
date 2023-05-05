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

        public StudentsController(ILogger<HomeController> logger, IStudentService studentService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager) : base(userManager, signInManager, roleManager)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> List()
        {
            if(User.IsInRole("Admin"))
            {
                var result = await _studentService.GetAll();
                return View(result);
            }
            else
            {

                AppUser currentUser = userManager.FindByNameAsync(User.Identity.Name).Result;
                var studentId = currentUser.StudentId;
                var student = await _studentService.GetStudentById(studentId.Value);
                var result = new List<Student>() {  student };
                return View(result);
            }

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new StudentViewModel());
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<JsonResult> GetById(int id)
        {
            var student = await _studentService.GetStudentById(id);
            var user = userManager.Users.FirstOrDefault(x => x.StudentId == id);
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
            var student = _studentService.GetStudentById(id).Result;
            if (student != null)
            {
                var user = userManager.Users.FirstOrDefault(x => x.StudentId == id);
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
                    return View("Create", studentViewModel);
                }
                else
                {
                    ViewBag.Message = "User Not Found";
                    return View(new StudentViewModel());
                }
            }
            else
            {
                ViewBag.Message = "Student Not Found";
                return View(new StudentViewModel());
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
                    await _studentService.Create(student);

                    AppUser user = new AppUser();
                    user.UserName = studentViewModel.UserName;
                    user.Email = studentViewModel.Email;
                    user.StudentId = student.StudentId;
                    user.NormalizedEmail = studentViewModel.Email.ToUpper();
                    user.NormalizedUserName = studentViewModel.UserName.ToUpper();

                    if (userManager.Users.Any(u => u.Email == studentViewModel.Email))
                    {
                        return Json(new { Message = "Email Address Has Already Been Taken", Status = false });
                    }

                    IdentityResult result = await userManager.CreateAsync(user, "Password_123");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                        return Json(new { Message = "New Student Created Successfully", Status = true });
                    }
                    else
                    {
                        var errorList = result.Errors.Select(x => x.Description);
                        return Json(new { Message = string.Join(",", errorList), Status = false });
                    }
                }
                else
                {
                    var student = _studentService.GetStudentById(studentViewModel.StudentId).Result;
                    if(student != null) {
                        student.FirstName = studentViewModel.FirstName;
                        student.LastName = studentViewModel.LastName;
                        student.Birthday = studentViewModel.Birthday;
                        await _studentService.Update(student);

                        var user = userManager.Users.FirstOrDefault(x => x.StudentId == studentViewModel.StudentId);
                        if(user != null)
                        {
                            user.UserName = studentViewModel.UserName;
                            user.Email = studentViewModel.Email;
                            // Apply the changes if any to the db
                            await userManager.UpdateAsync(user);
                        }
                        return Json(new { Message = "Student Updated Successfully", Status = true });

                    }
                    else
                    {
                        return Json(new { Message = "Student Not Found", Status = false });
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
            var student = _studentService.GetStudentById(studentId).Result;
            if (student != null)
            {
                await _studentService.Delete(studentId);
                var user = userManager.Users.Where(x => x.StudentId == studentId).FirstOrDefault();
                if (user != null)
                {
                    await userManager.RemoveFromRoleAsync(user, "User");
                    await userManager.DeleteAsync(user);
                }
                return Json(new { Status = true, Message = "Student Removed" });
            }
            else { return Json(new { Status = false, Message = "Student Could Not Found" }); }

        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
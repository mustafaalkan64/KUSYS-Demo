using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Core.ViewModels
{
    public class StudentCourseViewModel
    {
        public StudentCourseViewModel()
        {
            Courses = new List<CourseViewModel>();
        }
        public int StudentId { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }
    }

    public class CourseViewModel
    {
        public int Id { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public bool Checked { get; set; }
    }
}

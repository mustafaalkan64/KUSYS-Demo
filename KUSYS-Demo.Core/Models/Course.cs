using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Core.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public IList<StudentCourses> StudentCourses { get; set; }
    }
}

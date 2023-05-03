using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Core.Models
{
    public class Courses
    {
        [Key]
        public int CourseId { get; set; }
        public int CourseName { get; set; }
    }
}

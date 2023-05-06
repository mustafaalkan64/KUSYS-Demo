using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Core.ViewModels
{
    public class SaveStudentCourseViewModel
    {
        public int StudentId { get; set; }
        public int[] CourseIds { get; set; }
    }
}

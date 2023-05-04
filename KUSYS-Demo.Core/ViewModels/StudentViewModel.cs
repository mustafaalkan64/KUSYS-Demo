using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Core.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "FirstName is Required")]
        [StringLength(100, ErrorMessage = "FirstName Max Length Should be 100 ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "FirstName is Required")]
        [StringLength(100, ErrorMessage = "LastName Max Length Should be 100")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email Is Invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Birthday is Required")]
        public DateTime? Birthday { get; set; }
    }
}

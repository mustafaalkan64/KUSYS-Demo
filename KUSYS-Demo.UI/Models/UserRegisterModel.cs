using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KUSYS_Demo.UI.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Username is Required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is Required")]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Display(Name = "Phone:")]
        //[RegularExpression(@"^(0(\d{3}) (\d{3}) (\d{2}) (\d{2}))$", ErrorMessage = "Telefon numarası uygun formatta değil")]
        [Required(ErrorMessage = "Phone is Required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mail is Required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email is Not Valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

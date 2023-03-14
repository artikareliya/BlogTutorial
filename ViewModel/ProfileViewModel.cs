using System.ComponentModel.DataAnnotations;

namespace BlogTutorial.ViewModel
{
    public class ProfileViewModel
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Bio { get; set; }
        public IFormFile Image { get; set; }

        [DataType(DataType.Password)] //password in bullet type
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and ConfirmPassword Not Match")]
        public string ConfirmPassword {get; set;}
        public string Username { get; set; }
    }
}

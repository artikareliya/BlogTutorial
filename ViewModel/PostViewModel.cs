using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogTutorial.ViewModel
{
    public class PostViewModel
    {
        [Required(ErrorMessage ="Please Enter Title")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Please enter SubTitle")]
        public string SubTital { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        [DisplayName("Cover Image For BlogPost")]
        public IFormFile Image { get; set; }
        public string Slug { get; set; }
    }
}

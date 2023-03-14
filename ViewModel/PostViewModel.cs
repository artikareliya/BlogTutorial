using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogTutorial.ViewModel
{
    public class PostViewModel
    {
        public string Title { get; set; }
        public string SubTital { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        [DisplayName("Cover Image For BlogPost")]
        public IFormFile Image { get; set; }
        public string Slug { get; set; }
    }
}

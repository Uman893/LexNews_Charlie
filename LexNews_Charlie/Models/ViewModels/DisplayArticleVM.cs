using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LexNews_Charlie.Models.ViewModels
{
    public class DisplayArticleVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date of Publishment")]
        public DateTime DateStamp { get; set; } = DateTime.Now;

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string SubTitle { get; set; }
       
        [Display(Name = "Title Link")]
        public string LinkText { get; set; }

        [Required]
        public string CategoryId { get; set; } 
        public List<SelectListItem> CategoriesList { get; set; }     

        [DisplayName("File Name")]
        public string FileName { get; set; }
        public IFormFile File { get; set; }
        public Uri ImageLink { get; set; }

        public DisplayArticleVM()
        {
            CategoriesList = new List<SelectListItem>();
        }
    }
}

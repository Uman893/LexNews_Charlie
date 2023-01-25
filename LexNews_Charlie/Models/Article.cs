using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static NuGet.Packaging.PackagingConstants;

namespace LexNews_Charlie.Models
{
    public class Article
    {                
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Date of Publishment")]
        public DateTime DateStamp { get; set; } = DateTime.Now;
        
        [Display(Name = "Title Link")]
        public string LinkText { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name ="Subtitle")]
        public string SubTitle { get; set; }
        [Required]
        public string Content { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        [Required]
        [Display(Name ="Image Link")]
        public Uri ImageLink { get; set; }
        public string FileName { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public bool Archived { get; set; } = false; 
        public bool EditorsChoice { get; set; } = false;   

        //public List<Category> categories { get; set; }

    }
}

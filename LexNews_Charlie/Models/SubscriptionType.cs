using LexNews_Charlie.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace LexNews_Charlie.Models
{
    public class SubscriptionType
    {       
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }   
        public string TypeName { get; set; }
    } 
}

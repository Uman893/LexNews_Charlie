
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LexNews_Charlie.Models
{
    public class User : IdentityUser
    {       
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }
        public bool Employee { get; set; }
       
        public virtual ICollection<Subscription> SubscriptionsList { get; set; }

        public User()
        {

        }
        public User(string firstName, string lastName, string dateOfBirth, ICollection<Subscription> subscriptionsList)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            SubscriptionsList = subscriptionsList;
        }
    }
}

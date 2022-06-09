using Music_2.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.Data.Models
{
    public class UserViewModel
    {
        
        public Guid Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Dob")]
        public DateTime Dob { get; set; }
        public string CurrentRoom { get; set; }
        public IList<string> Roles { get; set; }
        public List<AppUser> Users { get; set; }
    }
}

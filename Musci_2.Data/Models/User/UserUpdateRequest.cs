using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.Data.Models.User
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }

        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        public string LastName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "SDT")]
        public string PhoneNumber { get; set; }
    }
}

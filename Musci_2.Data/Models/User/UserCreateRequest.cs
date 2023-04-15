using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.Data.Models
{
    public class UserCreateRequest
    {
        [Required(ErrorMessage = "Bạn phải nhập username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập password")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập email")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập tên người dùng")]
        public string Name { get; set; }
        public string Dob { get; set; }
    }
}

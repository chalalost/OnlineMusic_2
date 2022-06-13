using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.Data.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Mời nhập username!!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mời nhập password!!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.Data.Models.Singer
{
    public class SingerCreateRequest
    {
        [Required(ErrorMessage = "Bạn phải nhập tên ca sĩ")]
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public string Desciption { get; set; }
        public IFormFile Image { get; set; }
        public string Meta { get; set; }
        public string Code { get; set; }
    }
}

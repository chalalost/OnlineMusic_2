using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.Data.Models.Slide
{
    public class SlideCreateRequest
    {
        public string Name { get; set; }
        public string Description { set; get; }
        public string Url { get; set; }
        public IFormFile Image { get; set; }
       
    }
}

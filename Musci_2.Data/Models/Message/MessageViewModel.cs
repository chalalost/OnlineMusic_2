using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.Data.Models
{
    public class MessageViewModel
    {
        [Required]  
        public string Content { get; set; }
        public string Timestamp { get; set; }
        public string From { get; set; }
        [Required]
        public string Room { get; set; }
    }
}

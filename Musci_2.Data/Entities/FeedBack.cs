using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.Data.Entities
{
    public class FeedBack
    {
        public long ID { get; set; }
        public DateTime CreateDate { get; set; }
        public string FeedBackContent { get; set; }
        public string Email { get; set; }
    }
}

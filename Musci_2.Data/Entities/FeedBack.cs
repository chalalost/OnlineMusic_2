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

        [StringLength(100)]
        public string CreateDate { get; set; }

        [Column(TypeName = "ntext")]
        public string FeedBackContent { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
    }
}

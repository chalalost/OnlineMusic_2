using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.Data.Models.Singer
{
    public class SingerViewModel
    {
        public long SingerID { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public string Desciption { get; set; }
        public string Image { get; set; }

        public long? CategoryID { get; set; }
        public string Meta { get; set; }
        public string Code { get; set; }
        public int ViewCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Catalog.Category
{
   public class CategoryVm 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { set; get; }

        public bool IsShowOnHome { set; get; }

        public int? ParentId { get; set; }
    }
}

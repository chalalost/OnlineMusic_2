using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.Data.Models.Catalog.Categories
{
    public class CategoryCreateRequest
    {
        [Required(ErrorMessage = "Bạn phải nhập tên danh mục sản phẩm")]
        public string Name { get; set; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập id ngôn ngữ")]
        public string LanguageId { set; get; }
        public string SeoAlias { set; get; }
        public bool IsFeatured { get; set; }
    }
}

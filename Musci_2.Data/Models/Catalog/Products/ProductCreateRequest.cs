using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Music_2.Data.Models.Catalog.Products
{
    public class ProductCreateRequest
    {
        [Required(ErrorMessage = "Bạn phải nhập giá sản phẩm")]
        public decimal Price { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập giá sản phẩm gốc")]
        public decimal OriginalPrice { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập số lượng tồn kho")]
        public int Stock { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập mô tả sản phẩm")]
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập id ngôn ngữ")]
        public string LanguageId { set; get; }
        public bool? IsFeatured { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}

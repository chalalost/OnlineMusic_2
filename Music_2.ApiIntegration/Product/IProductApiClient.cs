using Music_2.Data.Models;
using Music_2.Data.Models.Catalog.Products;
using Music_2.Data.Models.CommonApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.ApiIntegration.Product
{
    public interface IProductApiClient
    {
        /// <summary>
        /// api phân trang sản phẩm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResult<ProductViewModel>> GetPagings(GetManageProductPagingRequest request);
        /// <summary>
        /// api tạo mới sản phẩm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<bool> CreateProduct(ProductCreateRequest request);
        /// <summary>
        /// api sửa san phẩm
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<bool> UpdateProduct(ProductUpdateRequest request);
        /// <summary>
        /// api gán danh mục cho sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
        /// <summary>
        /// api lấy sản phẩm theo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>

        Task<ProductViewModel> GetById(int id, string languageId);
        /// <summary>
        /// api lấy sp tốt
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="take"></param>
        /// <returns></returns>

        Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take);
        /// <summary>
        /// api lấy sp mới nhất
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="take"></param>
        /// <returns></returns>

        Task<List<ProductViewModel>> GetLatestProducts(string languageId, int take);
        /// <summary>
        /// api xóa sp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        Task<bool> DeleteProduct(int id);
    }
}

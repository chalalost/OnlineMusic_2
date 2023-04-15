using Microsoft.AspNetCore.Http;
using Music_2.Data.Models;
using Music_2.Data.Models.Catalog.ProductImages;
using Music_2.Data.Models.Catalog.Products;
using Music_2.Data.Models.CommonApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Product
{
    public interface IProductService
    {
        /// <summary>
        /// tạo mới sp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> Create(ProductCreateRequest request);
        /// <summary>
        /// sửa ttin sp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<int> Update(ProductUpdateRequest request);
        /// <summary>
        /// xóa sp
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>

        Task<int> Delete(int productId);
        /// <summary>
        /// lấy sp theo id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>

        Task<ProductViewModel> GetById(int productId, string languageId);
        /// <summary>
        /// sửa giá sp
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newPrice"></param>
        /// <returns></returns>

        Task<bool> UpdatePrice(int productId, decimal newPrice);
        /// <summary>
        /// sửa giá gốc sp
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="addedQuantity"></param>
        /// <returns></returns>

        Task<bool> UpdateStock(int productId, int addedQuantity);
        /// <summary>
        /// đếm số lượng view
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>

        Task AddViewcount(int productId);
        /// <summary>
        /// phân trang sp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        /// <summary>
        /// thêm ảnh
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        /// <summary>
        /// xóa ảnh
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>

        Task<int> RemoveImage(int imageId);
        /// <summary>
        /// sửa ảnh
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
        /// <summary>
        /// lấy ảnh theo id ảnh
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>

        Task<ProductImageViewModel> GetImageById(int imageId);
        /// <summary>
        /// lấy danh sách ảnh
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>

        Task<List<ProductImageViewModel>> GetListImages(int productId);
        /// <summary>
        /// phân trang sp theo danh mục sp
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request);
        /// <summary>
        /// gán danh mục sp
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
        /// <summary>
        /// lấy ra sp đang bán
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="take"></param>
        /// <returns></returns>

        Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take);
        /// <summary>
        /// lấy ra sp mới nhất
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="take"></param>
        /// <returns></returns>

        Task<List<ProductViewModel>> GetLatestProducts(string languageId, int take);
    }
}

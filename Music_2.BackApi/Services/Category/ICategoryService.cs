using Music_2.Data.Models;
using Music_2.Data.Models.Catalog.Categories;
using Music_2.Data.Models.CommonApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Category
{
    public interface ICategoryService
    {
        /// <summary>
        /// tạo mơi danh mục
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> Create(CategoryCreateRequest request);
        /// <summary>
        /// sửa danh mục
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> Update(CategoryUpdateRequest request);
        /// <summary>
        /// xóa danh mục
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>

        Task<int> Delete(int cateId);
        /// <summary>
        /// lấy toàn bộ ds danh mục
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        Task<List<CategoryViewModel>> GetAll(string languageId);
        /// <summary>
        /// lấy danh mục theo Id
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        Task<CategoryViewModel> GetById(string languageId, int id);
        /// <summary>
        /// phân trang 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResult<CategoryViewModel>> GetAllPaging(GetCategoriesPagingRequest request);
        
    }
}

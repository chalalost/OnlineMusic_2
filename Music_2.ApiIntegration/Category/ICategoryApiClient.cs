using Music_2.Data.Models;
using Music_2.Data.Models.Catalog.Categories;
using Music_2.Data.Models.CommonApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.ApiIntegration.Category
{
    public interface ICategoryApiClient
    {
        /// <summary>
        /// api tạo mới danh mục
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> Create(CategoryCreateRequest request);
        /// <summary>
        /// api lấy ds danh mục
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        Task<List<CategoryViewModel>> GetAll(string languageId);
        /// <summary>
        /// api phân trang danh mục
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResult<CategoryViewModel>> GetAllPaging(GetCategoriesPagingRequest request);
        /// <summary>
        /// api lấy danh mục theo id
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        Task<CategoryViewModel> GetById(string languageId, int id);
        /// <summary>
        /// api edit danh mục
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> Update(CategoryUpdateRequest request);
        /// <summary>
        /// api xóa danh mục
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(int id);
    }
}

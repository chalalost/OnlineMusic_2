using Music_2.Data.Models.Catalog.Categories;
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
    }
}

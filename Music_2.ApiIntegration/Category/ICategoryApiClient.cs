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
        Task<bool> Create(CategoryCreateRequest request);
        Task<List<CategoryViewModel>> GetAll(string languageId);
        Task<PagedResult<CategoryViewModel>> GetAllPaging(GetCategoriesPagingRequest request);

        Task<CategoryViewModel> GetById(string languageId, int id);
        Task<ApiResult<bool>> Update(int id, CategoryUpdateRequest request);
        Task<bool> Delete(int id);
    }
}

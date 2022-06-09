using Music_2.Data.Models;
using Music_2.Data.Models.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.ApiIntegration.Category
{
    public interface ICategoryApiClient
    {
        Task<ApiResult<bool>> Create(CategoryCreateRequest request);
        Task<ApiResult<CategoryViewModel>> GetAll(string languageId);

        Task<ApiResult<CategoryViewModel>> GetById(string languageId, int id);
    }
}

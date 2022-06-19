using Microsoft.EntityFrameworkCore;
using Music_2.Data.EF;
using Music_2.Data.Entities;
using Music_2.Data.Models;
using Music_2.Data.Models.Catalog.Categories;
using Music_2.Data.Models.CommonApi;
using Music_2.Data.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly OnlineMusicDbContext _context;
        public CategoryService(OnlineMusicDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CategoryCreateRequest request)
        {
            var languages = _context.Languages;
            var translations = new List<CategoryTranslation>();
            foreach (var language in languages)
            {
                if (language.Id == request.LanguageId)
                {
                    translations.Add(new CategoryTranslation()
                    {
                        Name = request.Name,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    });
                }
                else
                {
                    translations.Add(new CategoryTranslation()
                    {
                        Name = SystemConstants.ProductConstants.NA,
                        SeoDescription = SystemConstants.ProductConstants.NA,
                        SeoTitle = SystemConstants.ProductConstants.NA,
                        SeoAlias = SystemConstants.ProductConstants.NA,
                        LanguageId = language.Id
                    });
                }
            }
            var cate = new Data.Entities.Category()
            {
                Name = request.Name,
                CategoryTranslations = translations
            };

            _context.Categories.Add(cate);
            await _context.SaveChangesAsync();
            return cate.Id;

        }
        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };
            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                SeoDescription = x.ct.SeoDescription,
                SeoAlias = x.ct.SeoAlias,
                SeoTitle = x.ct.SeoTitle,
                LanguageId = languageId,
                Status = x.c.Status
            }).ToListAsync();
        }

        public async Task<CategoryViewModel> GetById(string languageId, int id)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId && c.Id == id
                        select new { c, ct };
            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                SeoDescription = x.ct.SeoDescription,
                SeoAlias = x.ct.SeoAlias,
                SeoTitle = x.ct.SeoTitle,
                LanguageId = languageId,
                Status = x.c.Status
            }).FirstOrDefaultAsync();
        }
        public async Task<PagedResult<CategoryViewModel>> GetAllPaging(GetCategoriesPagingRequest request)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == request.LanguageId
                        select new { c, ct };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.ct.Name.Contains(request.Keyword));
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CategoryViewModel()
                {
                    Id = x.c.Id,
                    Name = x.ct.Name,
                    SeoDescription = x.ct.SeoDescription,
                    SeoAlias = x.ct.SeoAlias,
                    SeoTitle = x.ct.SeoTitle,
                    LanguageId = request.LanguageId,
                    Status = x.c.Status
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<CategoryViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
        public async Task<int> Update(CategoryUpdateRequest request)
        {
            var product = await _context.Categories.FindAsync(request.Id);
            var cateTranslations = await _context.CategoryTranslations.FirstOrDefaultAsync(x => x.CategoryId == request.Id
            && x.LanguageId == request.LanguageId);

            if (product == null || cateTranslations == null) throw new OnlineMusicException($"Không tìm được danh mục id: {request.Id}");

            cateTranslations.Name = request.Name;
            cateTranslations.SeoAlias = request.SeoAlias;
            cateTranslations.SeoDescription = request.SeoDescription;
            cateTranslations.SeoTitle = request.SeoTitle;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(int cateId)
        {
            var cate = await _context.Categories.FindAsync(cateId);
            if (cate == null) throw new OnlineMusicException($"Không tìm được danh mục: {cateId}");
            _context.Categories.Remove(cate);

            return await _context.SaveChangesAsync();
        }
    }
}

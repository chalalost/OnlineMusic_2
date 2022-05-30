using Microsoft.EntityFrameworkCore;
using Music_2.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Catalog.Category;

namespace Music_2.BackApi.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly OnlineMusicDbContext _context;
        public CategoryService(OnlineMusicDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryVm>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).ToListAsync();
        }

        public async Task<CategoryVm> GetById(string languageId, int id)
        {
             var query = from c in _context.Categories
                          join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                          where ct.LanguageId == languageId && c.Id == id
                          select new { c, ct };
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).FirstOrDefaultAsync();
        }                         
    }
}

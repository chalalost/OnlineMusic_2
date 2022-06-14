using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Music_2.Data.EF;
using Music_2.Data.Models;
using Music_2.Data.Models.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Language
{
    public class LanguageService : ILanguageService
    {
        private readonly IConfiguration _config;
        private readonly OnlineMusicDbContext _context;

        public LanguageService(OnlineMusicDbContext context,
            IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        public async Task<ApiResult<List<LanguageViewModel>>> GetAll()
        {
            var languages = await _context.Languages.Select(x => new LanguageViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                IsDefault = x.IsDefault
            }).ToListAsync();
            return new ApiSuccessResult<List<LanguageViewModel>>(languages);
        }
    }
}

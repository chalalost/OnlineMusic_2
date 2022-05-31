using Microsoft.EntityFrameworkCore;
using Music_2.Data.EF;
using Music_2.Data.Models.Singer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Singer
{
    public class SingerService : ISingerService
    {
        private readonly OnlineMusicDbContext _context;

        public SingerService(OnlineMusicDbContext context)
        {
            _context = context;
        }

        public async Task<List<SingerViewModel>> GetAll()
        {
            var sing = await _context.Singers
                .Select(x => new SingerViewModel()
                {
                    SingerID = x.SingerID,
                    Name = x.Name,
                    MetaTitle = x.MetaTitle,
                    Desciption = x.Desciption,
                    Image = x.Image,
                    Code = x.Code,
                    ViewCount = x.ViewCount
                }).ToListAsync();
            return sing;
        }
    }
}

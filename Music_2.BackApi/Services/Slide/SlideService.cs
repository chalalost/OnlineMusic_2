using Microsoft.EntityFrameworkCore;
using Music_2.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Catalog.Slide;

namespace Music_2.BackApi.Services.Slide
{
    public class SlideService : ISlideService
    {
        private readonly OnlineMusicDbContext _context;
        public SlideService(OnlineMusicDbContext context)
        {
            _context = context;
        }

        public async Task<List<SlideVm>> GetAll()
        {
            var slides = await _context.Slides.OrderBy(x => x.SortOrder)
               .Select(x => new SlideVm()
               {
                   Id = x.Id,
                   Name = x.Name,
                   Description = x.Description,
                   Url = x.Url,
                   Image = x.Image
               }).ToListAsync();

            return slides;
        }
        
    }
}

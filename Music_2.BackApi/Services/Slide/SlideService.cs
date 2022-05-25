using Music_2.Data.EF;
using Music_2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Slide
{
    public class SlideService : ISlideService
    {
        private readonly OnlineMusicDbContext _context;
        public SlideService(OnlineMusicDbContext context)
        {
            _context = context;
        }
        public Task<List<SlideViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Music_2.Data.EF;
using Music_2.Data.Models.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.FeedBack
{
    public class FeedBackService : IFeedBackService
    {
        private readonly OnlineMusicDbContext _context;

        public FeedBackService(OnlineMusicDbContext context)
        {
            _context = context;
        }
        public async Task<List<FeedBackViewModel>> GetAll()
        {
            var fb = await _context.FeedBacks
                .Select(x => new FeedBackViewModel()
                {
                    ID = x.ID,
                    CreateDate = x.CreateDate,
                    Content = x.FeedBackContent,
                    Email = x.Email
                }).ToListAsync();
            return fb;
        }
    }
}

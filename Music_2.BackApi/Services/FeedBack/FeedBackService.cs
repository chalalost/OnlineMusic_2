using Microsoft.EntityFrameworkCore;
using Music_2.Data.EF;
using Music_2.Data.Models.CommonApi;
using Music_2.Data.Models.FeedBack;
using Music_2.Data.Models.Utils;
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

        public async Task<long> Create(FeedBackCreateRequest request)
        {
            var fb = new Data.Entities.FeedBack()
            {
                Email = request.Email,
                FeedBackContent = request.Content,
                CreateDate = DateTime.Now
            };
            _context.FeedBacks.Add(fb);
            await _context.SaveChangesAsync();
            return fb.ID;
        }

        public async Task<PagedResult<FeedBackViewModel>> GetAllPaging(GetFeedBackPagingRequest request)
        {
            var fb = from c in _context.FeedBacks
                     select new { c };

            if (!string.IsNullOrEmpty(request.Keyword))
                fb = fb.Where(x => x.c.Email.Contains(request.Keyword));
            //3. Paging
            int totalRow = await fb.CountAsync();

            var data = await fb.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new FeedBackViewModel()
                {
                    ID = x.c.ID,
                    CreateDate = x.c.CreateDate,
                    Email = x.c.Email,
                    Content = x.c.FeedBackContent
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<FeedBackViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;

        }
        public async Task<int> Delete(long Id)
        {
            var fb = await _context.FeedBacks.FindAsync(Id);
            if (fb == null) throw new OnlineMusicException($"Không tìm được danh mục: {Id}");
            _context.FeedBacks.Remove(fb);
            return await _context.SaveChangesAsync();
        }
    }
}

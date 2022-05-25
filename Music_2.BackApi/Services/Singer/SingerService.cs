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

        public Task<List<SingerViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

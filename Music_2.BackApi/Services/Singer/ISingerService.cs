using Music_2.Data.Models.Singer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Singer
{
    public interface ISingerService
    {
        Task<int> Create(SingerCreateRequest request);
        Task<List<SingerViewModel>> GetAll();
    }
}

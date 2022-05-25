using Music_2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Slide
{
    public interface ISlideService
    {
        Task<List<SlideViewModel>> GetAll();
    }
}

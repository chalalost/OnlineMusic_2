using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Catalog.Slide;

namespace Music_2.BackApi.Services.Slide
{
    public interface ISlideService
    {
        public Task<List<SlideVm>> GetAll();
    }
}

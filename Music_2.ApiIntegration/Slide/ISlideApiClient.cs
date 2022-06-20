using Music_2.Data.Models;
using Music_2.Data.Models.Slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.ApiIntegration.Slide
{
    public interface ISlideApiClient
    {
        Task<bool> Create(SlideCreateRequest request);
        Task<List<SlideViewModel>> GetAll();
    }
}

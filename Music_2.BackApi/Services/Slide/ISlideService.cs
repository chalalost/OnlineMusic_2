using Music_2.Data.Models;
using Music_2.Data.Models.Slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Slide
{
    public interface ISlideService
    {
        /// <summary>
        /// tạo mới slide
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> Create(SlideCreateRequest request);
        /// <summary>
        /// lấy ds slide
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<SlideViewModel>> GetAll();
    }
}

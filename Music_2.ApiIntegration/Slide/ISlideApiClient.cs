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
        /// <summary>
        /// api tạo mới slide
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> Create(SlideCreateRequest request);
        /// <summary>
        /// api lấy ds slide
        /// </summary>
        /// <returns></returns>
        Task<List<SlideViewModel>> GetAll();
        /// <summary>
        /// api xóa slide
        /// </summary>
        /// <param name="slideId"></param>
        /// <returns></returns>
        Task<bool> Delete(int slideId);
    }
}

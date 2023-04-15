using Music_2.Data.Models.CommonApi;
using Music_2.Data.Models.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.FeedBack
{
    public interface IFeedBackService
    {
        /// <summary>
        /// tạo mơi danh mục
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> Create(FeedBackCreateRequest request);
        /// <summary>
        /// lấy toàn bộ ds feed
        /// </summary>
        /// <returns></returns>
        Task<PagedResult<FeedBackViewModel>> GetAllPaging(GetFeedBackPagingRequest request);
        Task<int> Delete(long Id);
    }
}

using Music_2.Data.Models.CommonApi;
using Music_2.Data.Models.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.ApiIntegration.FeedBack
{
    public interface IFeedBackApiClient
    {
        /// <summary>
        /// api tạo mới feedback
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> Create(FeedBackCreateRequest request);
        /// <summary>
        /// api phân trang feedback
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResult<FeedBackViewModel>> GetAllPaging(GetFeedBackPagingRequest request);
        /// <summary>
        /// api xóa feedback
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(long id);
    }
}

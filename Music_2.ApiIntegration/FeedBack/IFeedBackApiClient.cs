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
        Task<bool> Create(FeedBackCreateRequest request);
        Task<PagedResult<FeedBackViewModel>> GetAllPaging(GetFeedBackPagingRequest request);
        Task<bool> Delete(long id);
    }
}

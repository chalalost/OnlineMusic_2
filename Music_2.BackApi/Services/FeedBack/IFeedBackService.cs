using Music_2.Data.Models.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.FeedBack
{
    public interface IFeedBackService
    {
        Task<List<FeedBackViewModel>> GetAll();
    }
}

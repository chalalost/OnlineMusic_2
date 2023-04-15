using Music_2.Data.Models.CommonApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.Data.Models.FeedBack
{
    public class GetFeedBackPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}

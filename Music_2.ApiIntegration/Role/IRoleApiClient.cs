using Music_2.Data.Models;
using Music_2.Data.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.ApiIntegration.Role
{
    public interface IRoleApiClient
    {
        /// <summary>
        /// api lấy ds rolde
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}

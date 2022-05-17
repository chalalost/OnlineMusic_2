using Music_2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services
{
    public interface IAuthenUserService
    {
        /// <summary>
        /// Authen Login
        /// </summary>
        Task<ApiResult<string>> Authencate(LoginRequest request);
    }
}

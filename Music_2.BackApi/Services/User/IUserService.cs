using Music_2.Data.Models;
using Music_2.Data.Models.CommonApi;
using Music_2.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// tạo mới user
        /// </summary>
        Task<ApiResult<bool>> Register(RegisterRequest request);
        /// <summary>
        /// sửa user
        /// </summary>
        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);
        /// <summary>
        /// phân trang user
        /// </summary>
        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);
        /// <summary>
        /// lấy user theo id
        /// </summary>
        Task<ApiResult<UserViewModel>> GetById(Guid id);
        /// <summary>
        /// Xóa user
        /// </summary>
        Task<ApiResult<bool>> Delete(Guid id);
        /// <summary>
        /// sửa role theo user
        /// </summary>
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
        /// <summary>
        /// lấy ds user
        /// </summary>
        Task<List<UserViewModel>> GetAll();
    }
}

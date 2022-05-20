using Music_2.Data.Entities;
using Music_2.Data.Models;
using Music_2.Data.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Role
{
    public interface IRoleService
    {
        /// <summary>
        /// Tạo mới role
        /// </summary>
        Task<ApiResult<bool>> Register(RoleRequest request);
        /// <summary>
        /// Xóa role
        /// </summary>
        Task<ApiResult<bool>> Remove(RemoveRoleRequest request);
        /// <summary>
        /// Lấy ds role
        /// </summary>
        Task<List<RoleViewModel>> GetAllRole();
        Task<IList<AppUser>> GetByIdRole(Guid id);
    }
}

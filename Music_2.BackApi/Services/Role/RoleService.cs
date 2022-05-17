using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Music_2.Data.Entities;
using Music_2.Data.Models;
using Music_2.Data.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        
        public async Task<List<RoleViewModel>> GetAllRole()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

            return roles;
        }
        
        public async Task<ApiResult<bool>> Register(RoleRequest request)
        {
            var name = _roleManager.FindByNameAsync(request.Name);
            if (name.Result != null)
            {
                return new ApiErrorResult<bool>("Role đã tồn tại");
            }
            var user = new AppRole()
            {
                Description = request.Name,
                Name = request.Name,
                NormalizedName = request.Name
            };
            var end = await _roleManager.CreateAsync(user);
            if (end.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }

            return new ApiErrorResult<bool>("Tạo không thành công");
        }
        

        public async Task<ApiResult<bool>> Remove(RemoveRoleRequest request)
        {
            var val = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (val == null)
            {
                return new ApiErrorResult<bool>("Xoas không thành công");
            }
            var end = await _roleManager.DeleteAsync(val);
            if (end.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }

            return new ApiErrorResult<bool>("Xoas không thành công");
        }
    }
}

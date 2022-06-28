using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Music_2.Data.Entities;
using Music_2.Data.Models;
using Music_2.Data.Models.CommonApi;
using Music_2.Data.Models.Role;
using Music_2.Data.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
        IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }
            var reult = await _userManager.DeleteAsync(user);
            if (reult.Succeeded)
                return new ApiSuccessResult<bool>();
            return new ApiErrorResult<bool>("Xóa không thành công");
        }
        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserViewModel>("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Dob = user.Dob,
                Id = user.Id,
                UserName = user.UserName,
                Roles = roles
            };
            return new ApiSuccessResult<UserViewModel>(userVm);
        }
        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)
                 || x.PhoneNumber.Contains(request.Keyword) || x.Email.Contains(request.Keyword) || x.Name.Contains(request.Keyword));
            }
            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserViewModel()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
            //4. Select and projection
            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserViewModel>>(pagedResult);
        }
        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var Email = _userManager.Users.FirstOrDefault(x => x.Email == request.Email);
            if (Email != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            var UserName = await _userManager.FindByNameAsync(request.UserName);
            if (UserName != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            var user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                Name = request.Name,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await new EmailSender().SendEmailAsync(request.Email, "Wellcome to Time Records"
                , $"You have successfully registered.");
            var end = await _userManager.ConfirmEmailAsync(user, token);
            if (end.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }
        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            return new ApiSuccessResult<bool>();
        }
        public async Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id))
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Dob = request.Dob;
            user.Email = request.Email;
            user.Name = request.Name;
            user.PhoneNumber = request.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }
        public async Task<List<UserViewModel>> GetAll()
        {
            var result = await _userManager.Users.Select(x => new UserViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName,
                Email = x.Email,
                Dob = x.Dob,
            }).ToListAsync();
            return result;
        }
        public async Task<ApiResult<string>> TokenForgotPass(InputModel Input)
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                return new ApiErrorResult<string>("ko thanh cong");
            }

            // Phát sinh Token để reset password
            // Token sẽ được kèm vào link trong email,
            // link dẫn đến trang /Account/ResetPassword để kiểm tra và đặt lại mật khẩu
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = System.Web.HttpUtility.UrlEncode(token);
            /*token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));*/
            return new ApiSuccessResult<string>(token);
        }
        public async Task<ApiResult<bool>> GetResetPasswordConfirm(string email, string token, string newpassword)
        {
            if (email == null || token == null)
            {
                return new ApiErrorResult<bool>(false.ToString());
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ApiErrorResult<bool>(false.ToString());
            }
            string code = token;
            var result = await _userManager.ResetPasswordAsync(user, code, newpassword);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>(false.ToString());
        }
    }
}

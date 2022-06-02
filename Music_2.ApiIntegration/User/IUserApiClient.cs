using Music_2.Data.Models;
using Music_2.Data.Models.CommonApi;
using Music_2.Data.Models.Role;
using Music_2.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Music_2.ApiIntegration
{
    public interface IUserApiClient
    {
        /// <summary>
        /// Authen lại ttin từ json
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        /// <summary>
        /// Phân trang lại ttin từ json
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPagings(GetUserPagingRequest request);
        /// <summary>
        /// Đăng ký user lại ttin từ json
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);
        /// <summary>
        /// Sửa lại user ttin từ json
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);
        /// <summary>
        /// Lấy user theo id ttin từ json
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        Task<ApiResult<UserViewModel>> GetById(Guid id);
        /// <summary>
        /// Xóa user ttin từ json
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        Task<ApiResult<bool>> Delete(Guid id);
        /// <summary>
        /// Phân role user ttin từ json
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
        Task<ApiResult<GetList<UserViewModel>>> GetAll();
        Task<ApiResult<List<RoleViewModel>>> GetAllRole();
        Task<ApiResult<string>> GetTokenForgotPass(InputModel Input);
        Task<ApiResult<string>> ResetPasswordConfirm(string email, string token, string newpassword);
    }
}

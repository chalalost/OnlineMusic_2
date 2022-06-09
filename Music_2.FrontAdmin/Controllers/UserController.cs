using ClosedXML.Excel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Music_2.ApiIntegration;
using Music_2.ApiIntegration.Role;
using Music_2.Data.EF;
using Music_2.Data.Models;
using Music_2.Data.Models.CommonApi;
using Music_2.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.FrontAdmin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IRoleApiClient _roleApiClient;
        private readonly OnlineMusicDbContext _context;

        public UserController(IUserApiClient userApiClient, IConfiguration configuration, IRoleApiClient roleApiClient, OnlineMusicDbContext context)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _roleApiClient = roleApiClient;
            _context = context;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUsersPagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            return View(result.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RegisterUser(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm mới người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UserUpdateRequest()
                {
                    Dob = user.Dob,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Id = id
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(new UserDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.Delete(request.Id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RoleAssign(request.Id, request);

            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật quyền thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.Id);

            return View(roleAssignRequest);
        }

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userObj = await _userApiClient.GetById(id);
            var roleObj = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            foreach (var role in roleObj.ResultObj)
            {
                roleAssignRequest.Roles.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = userObj.ResultObj.Roles.Contains(role.Name)
                });
            }
            return roleAssignRequest;
        }

        //export
        private DataTable GetUsersDetail()
        {
            var users = _context.Users.ToList();

            DataTable dtUser = new DataTable("Users");
            dtUser.Columns.AddRange(new DataColumn[6] { new DataColumn("ID"),
                                            new DataColumn("UserName"),
                                            new DataColumn("Name"),
                                            new DataColumn("Dob"),
                                            new DataColumn("Email"),
                                            new DataColumn("PhoneNumber")});
            foreach (var user in users)
            {
                dtUser.Rows.Add(user.Id, user.UserName, user.Name, user.Dob, user.Email, user.PhoneNumber);
            }

            return dtUser;
        }

        [HttpPost]
        public IActionResult Export()
        {
            var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var exportType = dictioneryexportType["Export"];
            var user = GetUsersDetail();
            switch (exportType)
            {
                case "Excel":
                    ExportToExcel(user);
                    break;
                    /*case "Csv":
                        ExportToCsv(user);
                        break;
                    case "Pdf":
                        ExportToPdf(user);
                        break;
                    case "Word":
                        ExportToWord(user);
                        break;
                    case "Json":
                        ExportToJson(user);
                        break;
                    case "Xml":
                        ExportToXML(user);
                        break;
                    case "Text":
                        ExportToText(user);
                        break;*/
            }
            return null;
        }
        private void ExportToExcel(DataTable user)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "UserName";
                worksheet.Cell(currentRow, 3).Value = "Name";
                worksheet.Cell(currentRow, 4).Value = "Dob";
                worksheet.Cell(currentRow, 5).Value = "Email";
                worksheet.Cell(currentRow, 6).Value = "PhoneNumber";

                for (int i = 0; i < user.Rows.Count; i++)
                {
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = user.Rows[i]["ID"];
                        worksheet.Cell(currentRow, 2).Value = user.Rows[i]["UserName"];
                        worksheet.Cell(currentRow, 3).Value = user.Rows[i]["Name"];
                        worksheet.Cell(currentRow, 4).Value = user.Rows[i]["Dob"];
                        worksheet.Cell(currentRow, 5).Value = user.Rows[i]["Email"];
                        worksheet.Cell(currentRow, 6).Value = user.Rows[i]["PhoneNumber"];

                    }
                }
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                Response.Clear();
                Response.Headers.Add("content-disposition", "attachment;filename=Users.xls");
                Response.ContentType = "application/xls";
                Response.Body.WriteAsync(content);
                Response.Body.Flush();
            }
        }

    }
}

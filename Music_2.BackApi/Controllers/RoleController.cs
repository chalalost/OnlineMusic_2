using Microsoft.AspNetCore.Mvc;
using Music_2.BackApi.Services.Role;
using Music_2.Data.Models;
using Music_2.Data.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAllRole();
            return Ok(roles);
        }
        [HttpPost]
        public async Task<IActionResult> CreaterRole(RoleRequest request)
        {
            var result = await _roleService.Register(request);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Remove(RemoveRoleRequest request)
        {
            var result = await _roleService.Remove(request);
            return Ok();
        }
    }
}

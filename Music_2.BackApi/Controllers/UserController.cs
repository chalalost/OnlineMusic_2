using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Music_2.BackApi.Models;
using Music_2.BackApi.Services;
using Music_2.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IAuthenUserService _authenService;
        public UserController(IAuthenUserService authenService)
        {
            _authenService = authenService;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authenService.Authencate(request);

            if (string.IsNullOrEmpty(result.ResultObj))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}

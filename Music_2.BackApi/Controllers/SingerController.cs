using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music_2.BackApi.Services.Singer;
using Music_2.Data.Models.Singer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SingerController : ControllerBase
    {
        private readonly ISingerService _singService;

        public SingerController(ISingerService singService)
        {
            _singService = singService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SingerCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _singService.Create(request);
            return Ok("ovanke");
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _singService.GetAll();
            return Ok(orders);
        }
    }
}

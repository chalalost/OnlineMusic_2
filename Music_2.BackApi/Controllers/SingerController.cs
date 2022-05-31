using Microsoft.AspNetCore.Mvc;
using Music_2.BackApi.Services.Singer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Controllers
{
    public class SingerController : ControllerBase
    {
        private readonly ISingerService _singService;

        public SingerController(ISingerService singService)
        {
            _singService = singService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _singService.GetAll();
            return Ok(orders);
        }
    }
}

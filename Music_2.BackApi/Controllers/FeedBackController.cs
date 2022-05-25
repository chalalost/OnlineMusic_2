using Microsoft.AspNetCore.Mvc;
using Music_2.BackApi.Services.FeedBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackService _fbService;

        public FeedBackController(IFeedBackService fbService)
        {
            _fbService = fbService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _fbService.GetAll();
            return Ok(orders);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music_2.BackApi.Services.FeedBack;
using Music_2.Data.Models.FeedBack;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FeedBackCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _fbService.Create(request);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetFeedBackPagingRequest request)
        {
            var fb = await _fbService.GetAllPaging(request);
            return Ok(fb);
        }

        [HttpDelete("{cateId}")]
        public async Task<IActionResult> Delete(long cateId)
        {
            var affectedResult = await _fbService.Delete(cateId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}

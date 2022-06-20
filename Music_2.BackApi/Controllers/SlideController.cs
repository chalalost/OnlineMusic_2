using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Music_2.BackApi.Services.Slide;
using Music_2.Data.Models.Slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_2.BackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SlideController : ControllerBase
    {
        private readonly ISlideService _slideService;

        public SlideController(ISlideService slideService)
        {
            _slideService = slideService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] SlideCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _slideService.Create(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var slide = await _slideService.GetAll();
            return Ok(slide);
        }
    }
}

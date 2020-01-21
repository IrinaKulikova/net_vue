using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PorkRibsAPI.API
{
    [Route("api/v1/[controller]/")]
    [ApiController]
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        private readonly List<string> _values = new List<string>() { "one", "two", "three" };


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_values);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id < 0 || id >= _values.Count)
            {
                return BadRequest();
            }

            return Ok(_values[id]);
        }
    }
}
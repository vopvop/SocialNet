using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Veises.Service.Example.Controllers
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[Produces("application/json", "application/xml")]
	public class ValuesController : ControllerBase
    {
		/// <summary>
		/// Get all values
		/// </summary>
		/// <returns></returns>
        // GET api/values
        [HttpGet(Name ="Get all values")]
		[ProducesResponseType(200, Type =typeof(IEnumerable<string>))]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
			return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
			return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
			return Ok();
        }
    }
}

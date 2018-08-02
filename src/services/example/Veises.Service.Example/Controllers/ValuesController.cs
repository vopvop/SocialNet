using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Veises.Service.Example.Controllers
{
	/// <summary>
	/// Default example Web API controller
	/// </summary>
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[Produces("application/json", "application/xml")]
	public sealed class ValuesController : ControllerBase
	{
		/// <summary>
		/// Get all values.
		/// </summary>
		/// <returns>Value.</returns>
		[HttpGet(Name = "Get all values")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<string>))]
		public IActionResult Get()
		{
			return Ok(new string[] {"value1", "value2"});
		}

		/// <summary>
		/// Get value by ID
		/// </summary>
		/// <param name="id">Value Identifier</param>
		/// <returns>Value</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		public IActionResult Get(int id)
		{
			return Ok($"value{id}");
		}

		/// <summary>
		/// Add new value.
		/// </summary>
		/// <param name="value">Value to update.</param>
		/// <returns>Operation result.</returns>
		[ProducesResponseType(200)]
		[HttpPost]
		public IActionResult Post([FromBody] string value)
		{
			return Ok();
		}

		/// <summary>
		/// Update existing value. 
		/// </summary>
		/// <param name="id">Existing value identifier.</param>
		/// <param name="value">New value.</param>
		/// <returns>Operation result.</returns>
		[ProducesResponseType(200)]
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] string value)
		{
			return Ok();
		}

		/// <summary>
		/// Delete existing value.
		/// </summary>
		/// <param name="id">Existing value Identifier.</param>
		/// <returns>Operation result.</returns>
		[ProducesResponseType(200)]
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			return Ok();
		}
	}
}
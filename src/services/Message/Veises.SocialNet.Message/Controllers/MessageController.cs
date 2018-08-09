using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Veises.SocialNet.Message.Adapters.Api;

namespace Veises.SocialNet.Message.Controllers
{
    /// <summary>
    /// Message service main WebAPI controller.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json", "application/xml")]
    public sealed class MessageController : ControllerBase
    {
        private readonly IMessageAdapter _messageAdapter;

        public MessageController(IMessageAdapter messageAdapter)
        {
            _messageAdapter = messageAdapter ?? throw new ArgumentNullException(nameof(messageAdapter));
        }

        /// <summary>
        /// Get all messages.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(MessageDto[]), (int) HttpStatusCode.OK)]
        public IActionResult Get()
        {
            var messages = _messageAdapter.GetAll();
            
            return Ok(messages);
        }

        /// <summary>
        /// Get message by ID.
        /// </summary>
        /// <param name="messageId">Message ID.</param>
        /// <returns>Single message with specified ID.</returns>
        [HttpGet("{messageId}")]
        [ProducesResponseType(typeof(MessageDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Get(Guid messageId)
        {
            var message = _messageAdapter.Get(messageId);

            return Ok(message);
        }

        /// <summary>
        /// Post a new message.
        /// </summary>
        /// <param name="content">New message content.</param>
        /// <returns>New message ID.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] string content)
        {
            var message = _messageAdapter.Post(content);
            
            return Ok(message);
        }

        /// <summary>
        /// Update existing message.
        /// </summary>
        /// <param name="id">Existing message ID.</param>
        /// <param name="content">A new message content.</param>
        /// <returns>Empty response.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Put(Guid id, [FromBody] string content)
        {
            _messageAdapter.Update(id, content);
            
            return NoContent();
        }

        /// <summary>
        /// Delete exising message.
        /// </summary>
        /// <param name="id">Existing message ID.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public IActionResult Delete(Guid id)
        {
            _messageAdapter.Delete(id);
            
            return NoContent();
        }
    }
}
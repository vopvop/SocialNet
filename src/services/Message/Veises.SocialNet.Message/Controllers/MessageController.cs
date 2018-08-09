using System;
using Microsoft.AspNetCore.Mvc;
using Veises.Common.Extensions;
using Veises.Common.Service.Log;
using Veises.SocialNet.Message.Adapters;

namespace Veises.SocialNet.Message.Controllers
{
    /// <summary>
    /// Message service main WebAPI controller.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json", "application/xml")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageAdapter _messageAdapter;

        private readonly ILogFor<MessageController> _log;

        public MessageController(IMessageAdapter messageAdapter, ILogFor<MessageController> log)
        {
            _messageAdapter = messageAdapter ?? throw new ArgumentNullException(nameof(messageAdapter));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Get all messages.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(string messageId)
        {
            if (messageId == null)
                return BadRequest("MessageId is empty");
            
            _log.WriteInfo($"Executing request by ID {messageId.Escaped()}");
            
            var message = _messageAdapter.Get(new MessageIdDto(Guid.Parse(messageId)));

            return Ok(message);
        }

        /// <summary>
        /// Post a new message.
        /// </summary>
        /// <param name="content">New message content.</param>
        /// <returns>New message ID.</returns>
        [HttpPost]
        [ProducesResponseType(200)]
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(MessageIdDto id, [FromBody] string content)
        {
            if (id == null)
                return BadRequest("Message Id is not defined.");
            
            _messageAdapter.Update(id, content);
            
            return NoContent();
        }

        /// <summary>
        /// Delete exising message.
        /// </summary>
        /// <param name="id">Existing message ID.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(MessageIdDto id)
        {
            if (id == null)
                return BadRequest("Message Id is not defined.");
            
            _messageAdapter.Delete(id);
            
            return Ok();
        }
    }
}
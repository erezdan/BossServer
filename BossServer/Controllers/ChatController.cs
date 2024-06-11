using BossServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BossServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("contexts")]
        public async Task<List<ChatContext>> GetChatContexts() =>
            await _chatService.GetChatContextsAsync();

        [HttpGet("contexts/{id:length(24)}")]
        public async Task<ActionResult<ChatContext>> GetChatContext(string id)
        {
            var context = await _chatService.GetChatContextAsync(id);

            if (context == null)
            {
                return NotFound();
            }

            return context;
        }

        [HttpPost("contexts")]
        public async Task<IActionResult> PostChatContext(ChatContext newChatContext)
        {
            await _chatService.CreateChatContextAsync(newChatContext);

            return CreatedAtAction(nameof(GetChatContext), new { id = newChatContext.Id }, newChatContext);
        }

        [HttpPut("contexts/{id:length(24)}")]
        public async Task<IActionResult> UpdateChatContext(string id, ChatContext updatedChatContext)
        {
            var context = await _chatService.GetChatContextAsync(id);

            if (context == null)
            {
                return NotFound();
            }

            updatedChatContext.Id = context.Id;

            await _chatService.UpdateChatContextAsync(id, updatedChatContext);

            return NoContent();
        }

        [HttpDelete("contexts/{id:length(24)}")]
        public async Task<IActionResult> DeleteChatContext(string id)
        {
            var context = await _chatService.GetChatContextAsync(id);

            if (context == null)
            {
                return NotFound();
            }

            await _chatService.RemoveChatContextAsync(id);

            return NoContent();
        }

        [HttpGet("messages/{contextId:length(24)}")]
        public async Task<List<ChatMessage>> GetChatMessages(string contextId) =>
            await _chatService.GetChatMessagesAsync(contextId);

        [HttpGet("messages/{id:length(24)}")]
        public async Task<ActionResult<ChatMessage>> GetChatMessage(string id)
        {
            var message = await _chatService.GetChatMessageAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        [HttpPost("messages")]
        public async Task<IActionResult> PostChatMessage(ChatMessage newChatMessage)
        {
            await _chatService.CreateChatMessageAsync(newChatMessage);

            return CreatedAtAction(nameof(GetChatMessage), new { id = newChatMessage.Id }, newChatMessage);
        }

        [HttpPut("messages/{id:length(24)}")]
        public async Task<IActionResult> UpdateChatMessage(string id, ChatMessage updatedChatMessage)
        {
            var message = await _chatService.GetChatMessageAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            updatedChatMessage.Id = message.Id;

            await _chatService.UpdateChatMessageAsync(id, updatedChatMessage);

            return NoContent();
        }

        [HttpDelete("messages/{id:length(24)}")]
        public async Task<IActionResult> DeleteChatMessage(string id)
        {
            var message = await _chatService.GetChatMessageAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            await _chatService.RemoveChatMessageAsync(id);

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;

namespace Flowspire.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost("send")]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        var senderId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var messageDto = new MessageDTO
        {
            SenderId = senderId,
            ReceiverId = request.ReceiverId,
            Content = request.Content
        };
        var result = await _messageService.SendMessageAsync(messageDto);
        return Ok(result);
    }

    [HttpGet("{otherUserId}")]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> GetMessages(string otherUserId)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var messages = await _messageService.GetMessagesAsync(userId, otherUserId);
        return Ok(messages);
    }
}

public class SendMessageRequest
{
    public string ReceiverId { get; set; }
    public string Content { get; set; }
}
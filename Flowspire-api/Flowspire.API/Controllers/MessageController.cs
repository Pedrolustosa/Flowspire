using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;
using Flowspire.API.Models;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController(IMessageService messageService) : ControllerBase
{
    private readonly IMessageService _messageService = messageService;

    [HttpPost("send")]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("{otherUserId}")]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> GetMessages(string otherUserId)
    {
        try
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var messages = await _messageService.GetMessagesAsync(userId, otherUserId);
            return Ok(messages);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
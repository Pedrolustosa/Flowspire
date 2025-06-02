using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.API.Common;
using Flowspire.Application.Common;
using Flowspire.API.Models.Messaging;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController(IMessageService messageService, ILogger<MessageController> logger) : ControllerBase
{
    private readonly IMessageService _messageService = messageService;
    private readonly ILogger<MessageController> _logger = logger;

    [HttpPost("send")]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> SendMessage([FromBody] MessageSendRequest request)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(request.ReceiverId) || string.IsNullOrWhiteSpace(request.Content))
                throw new ArgumentException(ErrorMessages.InvalidMessageData);

            var senderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(senderId))
                throw new UnauthorizedAccessException(ErrorMessages.UserNotAuthenticated);

            var messageDto = new MessageDTO
            {
                SenderId = senderId,
                ReceiverId = request.ReceiverId,
                Content = request.Content
            };

            var result = await _messageService.SendMessageAsync(messageDto);
            return result;
        }, _logger, this, SuccessMessages.MessageSent);

    [HttpGet("{otherUserId}")]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> GetMessages(string otherUserId)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(otherUserId))
                throw new ArgumentException(ErrorMessages.InvalidUserId);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException(ErrorMessages.UserNotAuthenticated);

            var messages = await _messageService.GetMessagesAsync(userId, otherUserId);
            return messages;
        }, _logger, this, SuccessMessages.MessagesRetrieved);
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;
using Flowspire.API.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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
            if (string.IsNullOrWhiteSpace(request.ReceiverId) || string.IsNullOrWhiteSpace(request.Content))
            {
                return BadRequest(new { Error = "ReceiverId and Content are required." });
            }

            var senderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(senderId))
            {
                return Unauthorized(new { Error = "User not authenticated." });
            }

            var messageDto = new MessageDTO
            {
                SenderId = senderId,
                ReceiverId = request.ReceiverId,
                Content = request.Content
            };

            var result = await _messageService.SendMessageAsync(messageDto);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpGet("{otherUserId}")]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> GetMessages(string otherUserId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(otherUserId))
            {
                return BadRequest(new { Error = "OtherUserId is required." });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized(new { Error = "User not authenticated." });
            }

            var messages = await _messageService.GetMessagesAsync(userId, otherUserId);
            return Ok(messages);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpPut("{messageId}")]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> UpdateMessage(int messageId, [FromBody] UpdateMessageRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return BadRequest(new { Error = "Content is required." });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized(new { Error = "User not authenticated." });
            }

            var message = await _messageService.GetByIdAsync(messageId);
            if (message == null)
            {
                return NotFound(new { Error = "Message not found." });
            }

            if (message.SenderId != userId)
            {
                return StatusCode(403, new { Error = "Access denied." });
            }

            var updatedMessage = await _messageService.UpdateMessageAsync(messageId, request.Content);
            return Ok(updatedMessage);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpDelete("{messageId}")]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> DeleteMessage(int messageId)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized(new { Error = "User not authenticated." });
            }

            var message = await _messageService.GetByIdAsync(messageId);
            if (message == null)
            {
                return NotFound(new { Error = "Message not found." });
            }

            if (message.SenderId != userId)
            {
                return StatusCode(403, new { Error = "Access denied." });
            }

            await _messageService.DeleteMessageAsync(messageId);
            return Ok(new { Message = "Message deleted successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpPost("{messageId}/read")]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> MarkAsRead(int messageId)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized(new { Error = "User not authenticated." });
            }

            var message = await _messageService.GetByIdAsync(messageId);
            if (message == null)
            {
                return NotFound(new { Error = "Message not found." });
            }

            if (message.ReceiverId != userId)
            {
                return StatusCode(403, new { Error = "Access denied." });
            }

            await _messageService.MarkMessageAsReadAsync(messageId);
            return Ok(new { Message = "Message marked as read successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}
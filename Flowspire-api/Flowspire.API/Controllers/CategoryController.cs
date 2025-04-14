using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpPost]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userId))
            categoryDTO.UserId = userId;

        await _categoryService.CreateAsync(categoryDTO);
        return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDTO.Id }, categoryDTO);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null)
            return NotFound("Category not found.");
        return Ok(category);
    }

    [HttpGet("user")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetCategoriesByUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User not identified.");

        var categories = await _categoryService.GetByUserIdAsync(userId);
        return Ok(categories);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
    {
        if (id != categoryDTO.Id)
            return BadRequest("ID mismatch.");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _categoryService.UpdateAsync(categoryDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }
}
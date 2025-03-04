using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;

namespace Flowspire.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDto)
    {
        categoryDto.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var result = await _categoryService.AddCategoryAsync(categoryDto);
        return Ok(result);
    }

    [HttpGet("user")]
    [Authorize(Roles = "Customer, FinancialAdvisor, Administrator")]
    public async Task<IActionResult> GetCategories()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var categories = await _categoryService.GetCategoriesByUserIdAsync(userId);
        return Ok(categories);
    }

    [HttpPut]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO categoryDto)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (categoryDto.UserId != userId)
            return Forbid();

        var result = await _categoryService.UpdateCategoryAsync(categoryDto);
        return Ok(result);
    }
}
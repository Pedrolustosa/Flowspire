using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.API.Common;
using Flowspire.Application.Common;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly ILogger<CategoryController> _logger = logger;

    [HttpPost]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException(ErrorMessages.UserNotIdentified);

            categoryDTO.UserId = userId;

            await _categoryService.CreateAsync(categoryDTO);
            return categoryDTO;
        }, _logger, this, SuccessMessages.CategoryCreated);

    [HttpGet("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetCategoryById(int id)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                throw new ArgumentException(ErrorMessages.CategoryNotFound);

            return category;
        }, _logger, this, SuccessMessages.CategoryRetrieved);

    [HttpGet("user")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetCategoriesByUser()
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException(ErrorMessages.UserNotIdentified);

            var categories = await _categoryService.GetByUserIdAsync(userId);
            return categories;
        }, _logger, this, SuccessMessages.CategoriesRetrieved);

    [HttpPut("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            if (id != categoryDTO.Id)
                throw new ArgumentException(ErrorMessages.IdMismatch);

            await _categoryService.UpdateAsync(categoryDTO);
        }, _logger, this, SuccessMessages.CategoryUpdated);

    [HttpDelete("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> DeleteCategory(int id)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            await _categoryService.DeleteAsync(id);
        }, _logger, this, SuccessMessages.CategoryDeleted);
}

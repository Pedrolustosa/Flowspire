using Flowspire.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Flowspire.Infra.Common;

namespace Flowspire.Infra.Repositories;

public class UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILogger<UserRepository> logger) : IUserRepository
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly ILogger<UserRepository> _logger = logger;

    public async Task<User?> FindByIdAsync(string id)
        => await RepositoryHelper.ExecuteAsync(() => _userManager.FindByIdAsync(id), _logger, nameof(FindByIdAsync));

    public async Task<User?> FindByEmailAsync(string email)
        => await RepositoryHelper.ExecuteAsync(() => _userManager.FindByEmailAsync(email), _logger, nameof(FindByEmailAsync));

    public async Task<IdentityResult> CreateAsync(User user, string password)
        => await RepositoryHelper.ExecuteAsync(() => _userManager.CreateAsync(user, password), _logger, nameof(CreateAsync));

    public async Task<IdentityResult> UpdateAsync(User user)
        => await RepositoryHelper.ExecuteAsync(() => _userManager.UpdateAsync(user), _logger, nameof(UpdateAsync));

    public async Task AddToRoleAsync(User user, string role)
        => await RepositoryHelper.ExecuteAsync(async () =>
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(user, role);
        }, _logger, nameof(AddToRoleAsync));

    public async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        => await RepositoryHelper.ExecuteAsync(() => _userManager.AddToRolesAsync(user, roles), _logger, nameof(AddToRolesAsync));

    public async Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
        => await RepositoryHelper.ExecuteAsync(() => _userManager.RemoveFromRolesAsync(user, roles), _logger, nameof(RemoveFromRolesAsync));

    public async Task<IList<string>> GetRolesAsync(User user)
        => await RepositoryHelper.ExecuteAsync(() => _userManager.GetRolesAsync(user), _logger, nameof(GetRolesAsync));

    public async Task<bool> IsInRoleAsync(User user, string role)
        => await RepositoryHelper.ExecuteAsync(() => _userManager.IsInRoleAsync(user, role), _logger, nameof(IsInRoleAsync));

    public async Task<IList<User>> GetUsersInRoleAsync(string role)
        => await RepositoryHelper.ExecuteAsync(() => _userManager.GetUsersInRoleAsync(role), _logger, nameof(GetUsersInRoleAsync));

    public async Task<bool> CheckPasswordAsync(User user, string password)
        => await RepositoryHelper.ExecuteAsync(() => _userManager.CheckPasswordAsync(user, password), _logger, nameof(CheckPasswordAsync));
}
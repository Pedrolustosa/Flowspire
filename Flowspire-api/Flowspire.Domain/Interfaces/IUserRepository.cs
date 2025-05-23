﻿using Flowspire.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Flowspire.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> FindByIdAsync(string id);
    Task<User?> FindByEmailAsync(string email);
    Task<IList<string>> GetRolesAsync(User user);
    Task<IList<User>> GetUsersInRoleAsync(string role);
    Task<bool> IsInRoleAsync(User user, string role);
    Task<bool> CheckPasswordAsync(User user, string password);

    Task<IdentityResult> CreateAsync(User user, string password);
    Task<IdentityResult> UpdateAsync(User user);
    Task AddToRoleAsync(User user, string role);
    Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles);
    Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles);
}
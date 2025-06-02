using Flowspire.Domain.Enums;

namespace Flowspire.API.Models.Users;

public class UserRoleAssignmentRequest
{
    public string UserId { get; set; }
    public UserRole Role { get; set; }
}

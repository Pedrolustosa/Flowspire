using Flowspire.Domain.Enums;

namespace Flowspire.API.Models
{
    public class AssignRoleRequest
    {
        public string UserId { get; set; }
        public UserRole Role { get; set; }
    }
}

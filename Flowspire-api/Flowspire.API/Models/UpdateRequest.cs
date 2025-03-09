using Flowspire.Domain.Enums;

namespace Flowspire.API.Models;

public class UpdateRequestWrapper
{
    public UpdateRequest requestWrapper { get; set; }
}

public class UpdateRequest
{
    public string FullName { get; set; }
    public List<UserRole> Roles { get; set; }
}

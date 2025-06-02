using Flowspire.Application.DTOs;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowspire.Application.Factories
{
    public interface IUserDtoFactory
    {
        UserDTO Create(User user, List<UserRole> roles);
    }
}

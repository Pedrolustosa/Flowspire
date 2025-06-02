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
    public class UserDtoFactory : IUserDtoFactory
    {
        public UserDTO Create(User u, List<UserRole> roles) => new()
        {
            Id           = u.Id,
            Email        = u.Email,
            FirstName    = u.Name.FirstName,
            LastName     = u.Name.LastName,
            PhoneNumber  = u.Phone.Value,
            BirthDate    = u.BirthDate,
            Gender       = u.Gender,
            AddressLine1 = u.Address.Line1,
            AddressLine2 = u.Address.Line2,
            City         = u.Address.City,
            State        = u.Address.State,
            Country      = u.Address.Country,
            PostalCode   = u.Address.PostalCode,
            Roles        = roles
        };
    }

}

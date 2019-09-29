using AddressBook.Model.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Api.Validations
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(a => a.StreetName).NotEmpty();
            RuleFor(a => a.City).NotEmpty();
            RuleFor(a => a.StateId).GreaterThan(0);
        }
    }
}

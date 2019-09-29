using AddressBook.Model.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Api.Validations
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.DateOfBirth).NotNull().LessThan(DateTime.Today);
            RuleFor(c => c.Address).NotNull().SetValidator(new AddressValidator());
        }
    }
}

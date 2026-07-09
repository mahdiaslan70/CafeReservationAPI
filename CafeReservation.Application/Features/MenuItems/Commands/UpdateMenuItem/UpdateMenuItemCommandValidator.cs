using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.MenuItems.Commands.UpdateMenuItem
{
    public class UpdateMenuItemCommandValidator : AbstractValidator<UpdateMenuItemCommand>
    {

        public UpdateMenuItemCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can not be empty !")
                .MaximumLength(50).WithMessage("Name con not be more than 50 characters !");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description can not be more than 200 characters !");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price should be greater than 0 !");
        }


    }
}

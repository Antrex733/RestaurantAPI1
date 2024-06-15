﻿using FluentValidation;

namespace RestaurantAPI1.Models.Validators
{
    public class RegisterUserDtoValidator: AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(8);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse) 
                    {
                        context.AddFailure("Email", "Thaat email is taken");
                    }
                });
        }
    }
}

using BusinessLogic.Model;
using FluentValidation;

namespace BusinessLogic.Validator
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Password).MaximumLength(255).NotEmpty().NotNull();
            RuleFor(x => x.Login).MaximumLength(255).NotEmpty().NotNull();
        }
    }
}
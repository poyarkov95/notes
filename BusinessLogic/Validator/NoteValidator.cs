using System;
using BusinessLogic.Model;
using FluentValidation;

namespace BusinessLogic.Validator
{
    public class NoteValidator : AbstractValidator<NoteModel>
    {
        public NoteValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.Details).MaximumLength(500);
        }
    }
}
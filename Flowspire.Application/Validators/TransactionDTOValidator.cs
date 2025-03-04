using FluentValidation;
using Flowspire.Application.DTOs;

namespace Flowspire.Application.Validators;
public class TransactionDTOValidator : AbstractValidator<TransactionDTO>
{
    public TransactionDTOValidator()
    {
        RuleFor(t => t.Description).NotEmpty().MaximumLength(200);
        RuleFor(t => t.Amount).NotEqual(0).WithMessage("O valor deve ser diferente de zero.");
        RuleFor(t => t.Date).NotEmpty();
        RuleFor(t => t.CategoryId).GreaterThan(0);
        RuleFor(t => t.UserId).NotEmpty();
    }
}
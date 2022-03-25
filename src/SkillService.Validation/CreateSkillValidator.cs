using FluentValidation;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Validation.Interfaces;

namespace SkillService.Validation
{
  public class CreateSkillValidator : AbstractValidator<string>, ICreateSkillValidator
  {
    public CreateSkillValidator(ISkillRepository skillRepository)
    {
      RuleFor(s => s.Trim())
        .NotEmpty().WithMessage("Name must not be empty or whitespace.")
        .MaximumLength(100).WithMessage("Name is too long");

      RuleFor(s => s)
        .MustAsync(async (name, _) => !await skillRepository
        .DoesNameExistAsync(name.Trim().ToLower()))
        .WithMessage("The skill already exists.");
    }
  }
}

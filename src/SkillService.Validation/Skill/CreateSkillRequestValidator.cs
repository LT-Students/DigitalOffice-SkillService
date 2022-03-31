using FluentValidation;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Validation.Interfaces;

namespace SkillService.Validation
{
  public class CreateSkillRequestValidator : AbstractValidator<string>, ICreateSkillRequestValidator
  {
    public CreateSkillRequestValidator(ISkillRepository skillRepository)
    {
      RuleFor(s => s.Trim())
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Name must not be empty or whitespace.")
        .MaximumLength(100).WithMessage("Name is too long.");

      RuleFor(s => s)
        .MustAsync(async (name, _) => !await skillRepository
        .DoesNameExistAsync(name.Trim().ToLower()))
        .WithMessage("The skill already exists.");
    }
  }
}

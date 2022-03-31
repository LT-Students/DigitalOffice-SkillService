using FluentValidation;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;
using LT.DigitalOffice.SkillService.Validation.Interfaces;

namespace SkillService.Validation
{
  public class CreateSkillRequestValidator : AbstractValidator<CreateSkillRequest>, ICreateSkillRequestValidator
  {
    public CreateSkillRequestValidator(ISkillRepository skillRepository)
    {
      RuleFor(s => s)
        .Cascade(CascadeMode.Stop)
        .Must(s => s.Name is not null).WithMessage("Name must not be null.")
        .Must(s => s.Name.Trim().Length > 0)
        .WithMessage("Name must not be empty or whitespace.")
        .Must(s => s.Name.Trim().Length < 101)
        .WithMessage("Name is too long.")
        .MustAsync(async (r, _) => !await skillRepository
        .DoesExistAsync(r.Name.Trim().ToLower()))
        .WithMessage("The skill already exists.");
    }
  }
}

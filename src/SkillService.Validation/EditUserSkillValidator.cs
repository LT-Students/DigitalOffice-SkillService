using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;
using LT.DigitalOffice.SkillService.Validation.Interfaces;

namespace LT.DigitalOffice.SkillService.Validation
{
  public class EditUserSkillValidator : AbstractValidator<EditUserSkillRequest>, IEditUserSkillValidator
  {
    public EditUserSkillValidator(
      ISkillRepository skillRepository)
    {
      RuleFor(r => r.SkillsToAdd)
        .Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("Skills list must not be null.")
        .Must(s => s.Distinct().ToList().Count == s.Count)
        .WithMessage("The skills can not be duplicated.");

      RuleFor(r => r.SkillsToRemove)
        .Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("Skills list must not be null.")
        .Must(s => s.Distinct().ToList().Count == s.Count)
        .WithMessage("The skills can not be duplicated."); ;

      RuleForEach(r => r.SkillsToAdd)
        .MustAsync(async (id, _) => await skillRepository.DoesNameExistAsync(id))
        .WithMessage("The skill does not exist.");

      RuleForEach(r => r.SkillsToRemove)
        .MustAsync(async (id, _) => await skillRepository.DoesNameExistAsync(id))
        .WithMessage("The skill does not exist.");
    }
  }
}

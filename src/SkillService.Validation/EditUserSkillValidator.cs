using System.Collections.Generic;
using FluentValidation;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Models.Db;
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
        .NotNull().WithMessage("Skills list must not be null.");

      RuleFor(r => r.SkillsToRemove)
        .NotNull().WithMessage("Skills list must not be null.");

      RuleForEach(r => r.SkillsToAdd)
        .MustAsync(async (id, _) => await skillRepository.DoesNameExistAsync(id))
        .WithMessage("The skill does not exist.");

      RuleForEach(r => r.SkillsToRemove)
        .MustAsync(async (id, _) => await skillRepository.DoesNameExistAsync(id))
        .WithMessage("The skill does not exist.");
    }
  }
}

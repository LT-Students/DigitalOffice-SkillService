using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;
using LT.DigitalOffice.SkillService.Validation.Interfaces;

namespace LT.DigitalOffice.SkillService.Validation
{
  public class EditUserSkillRequestValidator : AbstractValidator<(Guid id,EditUserSkillRequest request)>, IEditUserSkillRequestValidator
  {
    public EditUserSkillRequestValidator(
      ISkillRepository skillRepository,
      IUserSkillRepository userSkillRepository)
    {
      RuleFor(us => us.request)
        .Cascade(CascadeMode.Stop)
        .Must(r => r.SkillsToAdd is not null && r.SkillsToRemove is not null)
        .WithMessage("Skills lists must not be null.")
        .Must(r => r.SkillsToAdd.Distinct().ToList().Count == r.SkillsToAdd.Count
        && r.SkillsToRemove.Distinct().ToList().Count == r.SkillsToRemove.Count)
        .WithMessage("The skills can not be duplicated.")
        .Must(r => r.SkillsToAdd.Intersect(r.SkillsToRemove).ToList().Count == 0)
        .WithMessage("Skills to add and skills to remove must be different.")
        .MustAsync(async (r, _) => await skillRepository.DoesExistAsync(r.SkillsToAdd)
        && await skillRepository.DoesExistAsync(r.SkillsToRemove))
        .WithMessage("The skill does not exist.")
        .MustAsync(async (request, listSkill, _) => !listSkill.SkillsToAdd
        .Intersect(await userSkillRepository.GetUserSkillIdsAsync(request.id)).Any())
        .WithMessage("User already has these skills.");
    }
  }
}

using System;
using FluentValidation;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;

namespace LT.DigitalOffice.SkillService.Validation.Interfaces
{
  [AutoInject]
  public interface IEditUserSkillRequestValidator : IValidator<(Guid id, EditUserSkillRequest request)>
  {
  }
}

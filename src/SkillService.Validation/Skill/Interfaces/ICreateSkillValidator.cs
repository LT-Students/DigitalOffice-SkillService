using FluentValidation;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.SkillService.Validation.Interfaces
{
  [AutoInject]
  public interface ICreateSkillValidator : IValidator<string>
  {
  }
}

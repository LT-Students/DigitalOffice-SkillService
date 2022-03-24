using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.SkillService.Business.Commands.Skill.Interfaces
{
  [AutoInject]
  public interface ICreateSkillCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(string name);
  }
}

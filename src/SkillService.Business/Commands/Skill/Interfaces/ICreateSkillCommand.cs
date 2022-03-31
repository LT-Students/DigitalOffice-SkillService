using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;

namespace LT.DigitalOffice.SkillService.Business.Commands.Skill.Interfaces
{
  [AutoInject]
  public interface ICreateSkillCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateSkillRequest request);
  }
}

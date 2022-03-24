using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;

namespace LT.DigitalOffice.SkillService.Business.Commands.UserSkill.Interfaces
{
  [AutoInject]
  public interface IEditUserSkillCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId, EditUserSkillRequest request);
  }
}

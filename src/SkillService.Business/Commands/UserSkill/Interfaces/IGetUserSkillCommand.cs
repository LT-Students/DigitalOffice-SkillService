using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Models.Dto.Models;

namespace LT.DigitalOffice.SkillService.Business.Commands.UserSkill.Interfaces
{
  [AutoInject]
  public interface IGetUserSkillCommand
  {
    Task<OperationResultResponse<List<ShortSkillInfo>>> ExecuteAsync(Guid userId);
  }
}

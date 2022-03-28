using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SkillService.Models.Db;

namespace LT.DigitalOffice.SkillService.Data.Interfaces
{
  [AutoInject]
  public interface IUserSkillRepository
  {
    Task<List<Guid>> GetUserSkillAsync(Guid userId);

    Task AddUserSkillAsync(List<DbUserSkill> userSkill);

    Task RemoveUserSkillAsync(Guid userId, List<Guid> skillIds);
  }
}

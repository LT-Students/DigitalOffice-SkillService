using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.SkillService.Models.Dto.Requests.Filters;

namespace LT.DigitalOffice.SkillService.Data.Interfaces
{
  [AutoInject]
  public interface ISkillRepository
  {
    Task RemoveUnusedAsync();

    Task UpgradeTotalCountAsync(List<Guid> skillsIds);

    Task DowngradeTotalCountAsync(List<Guid> skillsIds);

    Task<bool> DoesExistAsync(string name);

    Task<bool> DoesExistAsync(List<Guid> ids);

    Task<Guid?> CreateAsync(DbSkill skill);

    Task<(List<DbSkill> dbSkills, int totalCount)> FindAsync(FindSkillFilter filter);
  }
}

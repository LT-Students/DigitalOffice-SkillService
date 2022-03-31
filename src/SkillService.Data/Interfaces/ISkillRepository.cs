using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SkillService.Models.Db;

namespace LT.DigitalOffice.SkillService.Data.Interfaces
{
  [AutoInject]
  public interface ISkillRepository
  {
    Task RemoveUnusedAsync();

    Task UpgradeTotalCountAsync(List<Guid> skillsIds);

    Task DowngradeTotalCountAsync(List<Guid> skillsIds);

    Task<bool> DoesNameExistAsync(string name);

    Task<bool> DoesNameExistAsync(Guid id);

    Task<Guid?> CreateAsync(DbSkill skill);
  }
}

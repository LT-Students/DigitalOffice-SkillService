﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SkillService.Models.Db;

namespace LT.DigitalOffice.SkillService.Data.Interfaces
{
  [AutoInject]
  public interface IUserSkillRepository
  {
    Task<List<Guid>> GetAsync(Guid userId);

    Task<List<DbUserSkill>> GetUserSkillsAsync(Guid userId);

    Task CreateAsync(List<DbUserSkill> usersSkills);

    Task<List<Guid>> RemoveAsync(Guid userId, List<Guid> skillsIds);
  }
}

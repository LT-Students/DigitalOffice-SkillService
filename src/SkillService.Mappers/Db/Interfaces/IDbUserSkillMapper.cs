﻿using System;
using System.Collections.Generic;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SkillService.Models.Db;

namespace LT.DigitalOffice.SkillService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbUserSkillMapper
  {
    List<DbUserSkill> Map(Guid userId, List<Guid> skillsIds);
  }
}

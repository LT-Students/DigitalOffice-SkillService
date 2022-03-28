using System;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SkillService.Models.Db;

namespace LT.DigitalOffice.SkillService.Mappers.Db.Interfsaces
{
  [AutoInject]
  public interface IDbUserSkillMapper
  {
    DbUserSkill Map(Guid userId, Guid skillId);
  }
}

using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.SkillService.Models.Dto.Models;

namespace LT.DigitalOffice.SkillService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface ISkillInfoMapper
  {
    SkillInfo Map(DbSkill dbSkill);
  }
}

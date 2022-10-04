using LT.DigitalOffice.SkillService.Mappers.Models.Interfaces;
using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.SkillService.Models.Dto.Models;

namespace LT.DigitalOffice.SkillService.Mappers.Models
{
  public class ShortSkillInfoMapper : IShortSkillInfoMapper
  {
    public ShortSkillInfo Map(DbSkill dbSkill)
    {
      if (dbSkill is null)
      {
        return null;
      }

      return new ShortSkillInfo
      {
        Id = dbSkill.Id,
        Name = dbSkill.Name
      };
    }
  }
}

using LT.DigitalOffice.SkillService.Mappers.Db.Interfsaces;
using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.SkillService.Models.Dto.Models;

namespace LT.DigitalOffice.SkillService.Mappers.Db
{
  public class SkillInfoMapper : ISkillInfoMapper
  {
    public SkillInfo Map(DbSkill dbSkill)
    {
      if (dbSkill is null)
      {
        return null;
      }

      return new SkillInfo
      {
        Id = dbSkill.Id,
        Name = dbSkill.Name,
        TotalCount = dbSkill.TotalCount,
      };
    }
  }
}

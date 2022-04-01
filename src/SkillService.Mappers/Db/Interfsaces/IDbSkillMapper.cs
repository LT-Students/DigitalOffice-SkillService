using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;

namespace LT.DigitalOffice.SkillService.Mappers.Db.Interfsaces
{
  [AutoInject]
  public interface IDbSkillMapper
  {
    DbSkill Map(CreateSkillRequest request);
  }
}

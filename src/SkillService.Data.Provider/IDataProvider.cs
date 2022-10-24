using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.EFSupport.Provider;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.SkillService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.SkillService.Data.Provider
{
  [AutoInject(InjectType.Scoped)]
  public interface IDataProvider : IBaseDataProvider
  {
    DbSet<DbSkill> Skills { get; set; }
    DbSet<DbUserSkill> UsersSkills { get; set; }
  }
}

using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.SkillService.Models.Db
{
  public class DbUserSkill
  {
    public const string TableName = "UsersSkills";

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SkillId { get; set; }
    public Guid AddedBy { get; set; }
    public DateTime AddedAtUtc { get; set; }

    public ICollection<DbSkill> Skills { get; set; }

    public DbUserSkill()
    {
      Skills = new HashSet<DbSkill>();
    }
  }
}

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
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public DbSkill Skill { get; set; }
  }
}

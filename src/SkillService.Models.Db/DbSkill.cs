using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.SkillService.Models.Db
{
  public class DbSkill
  {
    public const string TableName = "Skills";

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? BecameUnusedAtUtc { get; set; }
    public int TotalCount { get; set; }

    public ICollection<DbUserSkill> UsersSkills { get; set; }

    public DbSkill()
    {
      UsersSkills = new HashSet<DbUserSkill>();
    }
  }
}

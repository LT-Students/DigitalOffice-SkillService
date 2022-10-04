using System;
using System.Collections.Generic;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.SkillService.Mappers.Db.Interfaces;
using LT.DigitalOffice.SkillService.Models.Db;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.SkillService.Mappers.Db
{
  public class DbUserSkillMapper : IDbUserSkillMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbUserSkillMapper(
      IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public List<DbUserSkill> Map(Guid userId, List<Guid> skillsIds)
    {
      List<DbUserSkill> userSkills = new();

      foreach (Guid skillId in skillsIds)
      {
        userSkills.Add(new DbUserSkill
        {
          Id = new Guid(),
          UserId = userId,
          SkillId = skillId,
          CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
          CreatedAtUtc = DateTime.UtcNow,
        });
      }

      return userSkills;
    }
  }
}

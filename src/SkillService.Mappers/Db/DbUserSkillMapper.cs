using System;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.SkillService.Mappers.Db.Interfsaces;
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

    public DbUserSkill Map(Guid userId, Guid skillId)
    {
      return new DbUserSkill
      {
        Id = new Guid(),
        UserId = userId,
        SkillId = skillId,
        AddedBy = _httpContextAccessor.HttpContext.GetUserId(),
        AddedAtUtc = DateTime.UtcNow,
      };
    }
  }
}

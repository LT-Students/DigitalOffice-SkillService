using System;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.SkillService.Mappers.Db.Interfsaces;
using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.SkillService.Mappers.Db
{
  public class DbSkillMapper : IDbSkillMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbSkillMapper(
      IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbSkill Map(CreateSkillRequest request)
    {
      return new DbSkill
      {
        Id = Guid.NewGuid(),
        Name = request.Name.Trim().ToLower(),
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow,
        BecameUnusedAtUtc = DateTime.UtcNow,
        TotalCount = 0
      };
    }
  }
}

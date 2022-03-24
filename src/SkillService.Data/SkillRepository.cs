using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Data.Provider;
using LT.DigitalOffice.SkillService.Models.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.SkillService.Data
{
  public class SkillRepository : ISkillRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SkillRepository(IDataProvider provider, IHttpContextAccessor httpContextAccessor)
    {
      _provider = provider;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid?> CreateAsync(string name)
    {
      name = name.Trim();

      DbSkill existSkill = await _provider.Skills.FirstOrDefaultAsync(s => s.Name == name);

      if (existSkill is not null)
      {
        return null;
      }

      DbSkill skill = new DbSkill
      {
        Id = Guid.NewGuid(),
        Name = name,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow,
        TotalCount = 0
      };

      _provider.Skills.Add(skill);
      await _provider.SaveAsync();

      return skill.Id;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Data.Provider;
using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.SkillService.Models.Dto.Requests.Filters;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.SkillService.Data
{
  public class SkillRepository : ISkillRepository
  {
    private readonly IDataProvider _provider;

    private IQueryable<DbSkill> CreateFindPredicates(
      FindSkillFilter filter,
      IQueryable<DbSkill> dbSkill)
    {
      if (!string.IsNullOrEmpty(filter.NameIncludeSubstring))
      {
        dbSkill = dbSkill.Where(
          skill =>
            skill.Name.Contains(filter.NameIncludeSubstring));
      }

      if (filter.IsAscendingSort.HasValue)
      {
        dbSkill = filter.IsAscendingSort.Value
          ? dbSkill.OrderBy(skill => skill.Name)
          : dbSkill.OrderByDescending(skill => skill.Name);
      }

      else
      {
        dbSkill = dbSkill.OrderByDescending(skill => skill.TotalCount);
      }

      return dbSkill;
    }

    public SkillRepository(
      IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task RemoveUnusedAsync()
    {
      List<DbSkill> skills = await _provider.Skills
        .Where(s => s.BecameUnusedAtUtc != null).ToListAsync();
      skills = skills
        .Where(s => (DateTime.UtcNow - (DateTime)s.BecameUnusedAtUtc).TotalDays > 1)
        .ToList();

      _provider.Skills.RemoveRange(skills);
      await _provider.SaveAsync();
    }

    public async Task UpgradeTotalCountAsync(List<Guid> skillsIds)
    {
      foreach(Guid skillId in skillsIds)
      {
        DbSkill skill = await _provider.Skills.FirstOrDefaultAsync(s => s.Id == skillId);

        if (skill is not null)
        {
          skill.TotalCount++;

          if (skill.BecameUnusedAtUtc is not null)
          {
            skill.BecameUnusedAtUtc = null;
          }
        }
      }

      await _provider.SaveAsync();
    }

    public async Task DowngradeTotalCountAsync(List<Guid> skillsIds)
    {
      foreach (Guid skillId in skillsIds)
      {
        DbSkill skill = await _provider.Skills.FirstOrDefaultAsync(s => s.Id == skillId);

        if (skill is not null)
        {
          skill.TotalCount--;

          if (skill.TotalCount == 0)
          {
            skill.BecameUnusedAtUtc = DateTime.UtcNow;
          }
        }
      }

      await _provider.SaveAsync();
    }

    public async Task<bool> DoesExistAsync(string name)
    {
      return await _provider.Skills.AnyAsync(s => s.Name == name);
    }

    public async Task<bool> DoesExistAsync(List<Guid> skillsIds)
    {
      foreach (Guid id in skillsIds)
      {
        if (!await _provider.Skills.AnyAsync(s => s.Id == id))
        {
          return false;
        }
      }

      return true;
    }

    public async Task<Guid?> CreateAsync(DbSkill skill)
    {
      if (skill is null)
      {
        return null;
      }

      _provider.Skills.Add(skill);
      await _provider.SaveAsync();

      return skill.Id;
    }
  
    public async Task<(List<DbSkill> dbSkill, int totalCount)> FindAsync(FindSkillFilter filter)
    {
      if (filter is null)
      {
        return (null, default);
      }

      IQueryable<DbSkill> dbSkill = CreateFindPredicates(
        filter,
        _provider.Skills.AsQueryable());

      return (
        await dbSkill.Skip(filter.SkipCount).Take(filter.TakeCount).ToListAsync(),
        await dbSkill.CountAsync());
    }
  }
}

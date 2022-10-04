using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Data.Provider;
using LT.DigitalOffice.SkillService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.SkillService.Data
{
  public class UserSkillRepository : IUserSkillRepository
  {
    private readonly IDataProvider _provider;

    public UserSkillRepository(
      IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<List<Guid>> GetUserSkillIdsAsync(Guid userId)
    {
      return await _provider.UsersSkills
        .Where(us => us.UserId == userId).Select(us => us.SkillId).ToListAsync();
    }

    public async Task<List<DbSkill>> GetAsync(Guid userId)
    {
      return await _provider.UsersSkills
        .Where(us => us.UserId == userId)
        .Include(us => us.Skill)
        .Select(us => us.Skill)
        .ToListAsync();
    }

    public async Task CreateAsync(List<DbUserSkill> usersSkills)
    {
      _provider.UsersSkills.AddRange(usersSkills);
      await _provider.SaveAsync();
    }

    public async Task<List<Guid>> RemoveAsync(Guid userId, List<Guid> skillsIds)
    {
      List<DbUserSkill> usersSkills;

      if (skillsIds is null)
      {
        usersSkills = await _provider.UsersSkills
          .Where(us => us.UserId == userId).ToListAsync();
      }
      else
      {
        usersSkills = await _provider.UsersSkills
          .Where(us => us.UserId == userId && skillsIds.Contains(us.SkillId)).ToListAsync();
      }

      _provider.UsersSkills.RemoveRange(usersSkills);
      await _provider.SaveAsync();

      return usersSkills.Select(us => us.SkillId).ToList();
    }
  }
}

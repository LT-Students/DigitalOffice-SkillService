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

    public async Task<List<Guid>> GetUserSkillAsync(Guid userId)
    {
      return await _provider.UsersSkills
        .Where(us => us.UserId == userId).Select(us => us.SkillId).ToListAsync();
    }

    public async Task AddUserSkillAsync(List<DbUserSkill> userSkills)
    {
      _provider.UsersSkills.AddRange(userSkills);
      await _provider.SaveAsync();
    }

    public async Task RemoveUserSkillAsync(Guid userId, List<Guid> skillIds)
    {
      List<DbUserSkill> userSkills = await _provider.UsersSkills
        .Where(us => us.UserId == userId && skillIds.Contains(us.SkillId)).ToListAsync();

      _provider.UsersSkills.RemoveRange(userSkills);
      await _provider.SaveAsync();
    }
  }
}

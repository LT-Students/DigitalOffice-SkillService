﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Data.Provider;
using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.SkillService.Data
{
  public class UserSkillRepository : IUserSkillRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserSkillRepository(
      IDataProvider provider,
      IHttpContextAccessor httpContextAccessor)
    {
      _provider = provider;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> EditAsync(Guid userId, EditUserSkillRequest request)
    {
      List<Guid> existSkills = await _provider.UsersSkills
        .Where(us => us.UserId == userId).Select(us => us.SkillId).ToListAsync();
      List<Guid> conflictSkills = request.SkillsToAdd.Intersect(request.SkillsToRemove).ToList();

      request.SkillsToRemove = request.SkillsToRemove.Except(conflictSkills).ToList();
      request.SkillsToAdd = request.SkillsToAdd.Except(conflictSkills).ToList();

      foreach (Guid skillId in request.SkillsToAdd)
      {
        if (!existSkills.Contains(skillId))
        {
          DbSkill skill = await _provider.Skills.FirstOrDefaultAsync(s => s.Id == skillId);

          //mapper
          _provider.UsersSkills.Add(new DbUserSkill
          {
            Id = new Guid(),
            UserId = userId,
            SkillId = skillId,
            AddedBy = _httpContextAccessor.HttpContext.GetUserId(),
            AddedAtUtc = DateTime.UtcNow,
          });

          skill.TotalCount++;

          if (skill.BecameUnusedAtUtc is not null)
          {
            skill.BecameUnusedAtUtc = null;
          }
        }
      }

      foreach (Guid skillId in request.SkillsToRemove)
      {
        if (existSkills.Contains(skillId))
        {
          DbSkill skill = await _provider.Skills.FirstOrDefaultAsync(s => s.Id == skillId);
          DbUserSkill userSkill = await _provider.UsersSkills
            .FirstOrDefaultAsync(us => us.SkillId == skillId && us.UserId == userId);

          _provider.UsersSkills.Remove(userSkill);

          skill.TotalCount--;

          if (skill.TotalCount == 0)
          {
            skill.BecameUnusedAtUtc = DateTime.UtcNow;
          }
        }
      }

      await _provider.SaveAsync();

      return true;
    }
  }
}

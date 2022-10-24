using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Business.Commands.Skill.Interfaces;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Mappers.Models.Interfaces;
using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.SkillService.Models.Dto.Models;
using LT.DigitalOffice.SkillService.Models.Dto.Requests.Filters;

namespace LT.DigitalOffice.SkillService.Business.Commands.Skill
{
  public class FindSkillCommand : IFindSkillCommand
  {
    private readonly ISkillRepository _skillRepository;
    private readonly ISkillInfoMapper _skillInfoMapper;

    public FindSkillCommand(
      ISkillRepository skillRepository,
      ISkillInfoMapper skillInfoMapper)
    {
      _skillRepository = skillRepository;
      _skillInfoMapper = skillInfoMapper;
    }

    public async Task<FindResultResponse<SkillInfo>> ExecuteAsync(FindSkillFilter filter)
    {
      FindResultResponse<SkillInfo> response = new();

      (List<DbSkill> dbSkills, int totalCount) = await _skillRepository.FindAsync(filter);

      response.Body = dbSkills?.Select(dbSkill => _skillInfoMapper.Map(dbSkill)).ToList();
      response.TotalCount = totalCount;

      return response;
    }
  }
}

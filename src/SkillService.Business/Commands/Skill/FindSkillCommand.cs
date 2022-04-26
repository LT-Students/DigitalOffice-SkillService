using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using LT.DigitalOffice.SkillService.Business.Commands.Skill.Interfaces;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Models.Dto.Models;
using LT.DigitalOffice.SkillService.Mappers.Db.Interfsaces;
using LT.DigitalOffice.SkillService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.Kernel.Enums;

namespace LT.DigitalOffice.SkillService.Business.Commands.Skill
{
  public class FindSkillCommand : IFindSkillCommand
  {
    private readonly IBaseFindFilterValidator _baseFindValidator;
    private readonly ISkillRepository _skillRepository;
    private readonly ISkillInfoMapper _skillInfoMapper;
    private readonly IResponseCreator _responseCreator;

    public FindSkillCommand(
      IBaseFindFilterValidator baseFindValidator,
      ISkillRepository skillRepository,
      ISkillInfoMapper skillInfoMapper,
      IResponseCreator responseCreator)
    {
      _baseFindValidator = baseFindValidator;
      _skillRepository = skillRepository;
      _skillInfoMapper = skillInfoMapper;
      _responseCreator = responseCreator;
    }

    public async Task<FindResultResponse<SkillInfo>> ExecuteAsync(FindSkillFilter filter)
    {
      if (!_baseFindValidator.ValidateCustom(filter, out List<string> errors))
      {
        return _responseCreator.CreateFailureFindResponse<SkillInfo>(HttpStatusCode.BadRequest, errors);
      }
      FindResultResponse<SkillInfo> response = new();

      (List<DbSkill> dbSkills, int totalCount) = await _skillRepository.FindAsync(filter);

      if (!dbSkills.Equals(null)) { 
        response.Body = dbSkills.Select(dbSkill => _skillInfoMapper.Map(dbSkill)).ToList();
        response.TotalCount = totalCount;
      }

      return response;
    }
  }
}

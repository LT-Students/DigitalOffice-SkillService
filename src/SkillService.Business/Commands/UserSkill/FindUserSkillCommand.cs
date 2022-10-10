using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Business.Commands.UserSkill.Interfaces;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Mappers.Models.Interfaces;
//using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.SkillService.Models.Dto.Models;
using MassTransit.Initializers;

namespace LT.DigitalOffice.SkillService.Business.Commands.UserSkill;

public class FindUserSkillCommand : IFindUserSkillCommand
{
  private readonly IUserSkillRepository _userSkillRepository;
  private readonly IShortSkillInfoMapper _shotrSkillInfoMapper;

  public FindUserSkillCommand(
    IUserSkillRepository userSkillRepository,
    IShortSkillInfoMapper shotrSkillInfoMapper)
  {
    _userSkillRepository = userSkillRepository;
    _shotrSkillInfoMapper = shotrSkillInfoMapper;
  }

  public async Task<OperationResultResponse<List<ShortSkillInfo>>> ExecuteAsync(Guid userId)
  {
    return new OperationResultResponse<List<ShortSkillInfo>>(
      body: (await _userSkillRepository.FindAsync(userId)).Select(_shotrSkillInfoMapper.Map).ToList());
  }
}

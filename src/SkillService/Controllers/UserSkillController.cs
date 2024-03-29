﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Business.Commands.UserSkill.Interfaces;
using LT.DigitalOffice.SkillService.Models.Dto.Models;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.SkillService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class UserSkillController : ControllerBase
  {
    [HttpPut("edit")]
    public async Task<OperationResultResponse<bool>> EditAsync(
      [FromServices] IEditUserSkillCommand command,
      [FromQuery] Guid userId,
      [FromBody] EditUserSkillRequest request)
    {
      return await command.ExecuteAsync(userId, request);
    }

    [HttpGet("find")]
    public async Task<OperationResultResponse<List<ShortSkillInfo>>> FindSkillAsync(
      [FromServices] IFindUserSkillCommand command,
      [FromQuery] Guid userId)
    {
      return await command.ExecuteAsync(userId);
    }
  }
}

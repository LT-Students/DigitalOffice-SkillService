using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Business.Commands.Skill.Interfaces;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.SkillService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class SkillController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromServices] ICreateSkillCommand command,
      [FromBody] CreateSkillRequest request)
    {
      return await command.ExecuteAsync(request);
    }
  }
}

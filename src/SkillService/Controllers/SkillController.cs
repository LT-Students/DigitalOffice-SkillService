using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Business.Commands.Skill.Interfaces;
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
      [FromQuery] string name)
    {
      return await command.ExecuteAsync(name);
    }
  }
}

using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Models.Dto.Models;
using LT.DigitalOffice.SkillService.Models.Dto.Requests.Filters;

namespace LT.DigitalOffice.SkillService.Business.Commands.Skill.Interfaces
{
  [AutoInject]
  public interface IFindSkillCommand
  {
    Task<FindResultResponse<SkillInfo>> ExecuteAsync(FindSkillFilter filter);
  }
}

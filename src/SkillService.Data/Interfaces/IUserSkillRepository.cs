using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;

namespace LT.DigitalOffice.SkillService.Data.Interfaces
{
  [AutoInject]
  public interface IUserSkillRepository
  {
    Task RemoveUnusedSkillsAsync();
    Task<bool> EditAsync(Guid userId, EditUserSkillRequest request);
  }
}

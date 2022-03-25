using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.SkillService.Data.Interfaces
{
  [AutoInject]
  public interface ISkillRepository
  {
    Task<bool> DoesNameExistAsync(string name);
    Task<bool> DoesNameExistAsync(Guid id);
    Task<Guid?> CreateAsync(string name);
  }
}

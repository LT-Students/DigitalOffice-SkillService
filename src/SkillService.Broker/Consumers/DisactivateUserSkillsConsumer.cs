using System.Threading.Tasks;
using LT.DigitalOffice.Models.Broker.Publishing;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using MassTransit;

namespace LT.DigitalOffice.SkillService.Broker.Consumers
{
  public class DisactivateUserSkillsConsumer : IConsumer<IDisactivateUserPublish>
  {
    private readonly IUserSkillRepository _userSkillRepository;
    private readonly ISkillRepository _skillRepository;

    public DisactivateUserSkillsConsumer(
      IUserSkillRepository userSkillRepository,
      ISkillRepository skillRepository)
    {
      _userSkillRepository = userSkillRepository;
      _skillRepository = skillRepository;
    }

    public async Task Consume(ConsumeContext<IDisactivateUserPublish> context)
    {
      await _skillRepository.DowngradeTotalCountAsync(
        await _userSkillRepository.RemoveAsync(
          context.Message.UserId,
          null));
    }
  }
}

using System.Threading.Tasks;
using LT.DigitalOffice.Models.Broker.Publishing;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using MassTransit;

namespace LT.DigitalOffice.SkillService.Broker.Consumers
{
  public class DisactivateUserSkillConsumer : IConsumer<IDisactivateUserPublish>
  {
    private readonly IUserSkillRepository _repository;

    public DisactivateUserSkillConsumer(IUserSkillRepository repository)
    {
      _repository = repository;
    }

    public async Task Consume(ConsumeContext<IDisactivateUserPublish> context)
    {
      await _repository.RemoveAsync(
        context.Message.UserId, 
        null);
    }
  }
}

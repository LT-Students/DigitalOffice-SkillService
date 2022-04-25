using LT.DigitalOffice.Kernel.BrokerSupport.Attributes;
using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;
using LT.DigitalOffice.Models.Broker.Publishing;
using LT.DigitalOffice.Models.Broker.Requests.Skill;

namespace LT.DigitalOffice.SkillService.Models.Dto.Configuration
{
  public class RabbitMqConfig : BaseRabbitMqConfig
  {
    [AutoInjectRequest(typeof(IGetUserSkillsRequest))]
    public string GetUserSkillsEndpoint { get; set; }

    [AutoInjectRequest(typeof(IDisactivateUserPublish))]
    public string DisactivateUserEndpoint { get; set; }
  }
}

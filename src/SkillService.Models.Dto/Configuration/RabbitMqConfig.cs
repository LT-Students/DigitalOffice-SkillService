using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;

namespace LT.DigitalOffice.SkillService.Models.Dto.Configuration
{
  public class RabbitMqConfig : BaseRabbitMqConfig
  {
    public string GetUserSkillsEndpoint { get; set; }

    public string DisactivateUserEndpoint { get; set; }
  }
}

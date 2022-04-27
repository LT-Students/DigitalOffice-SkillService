using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Models.Broker.Models.Skill;
using LT.DigitalOffice.Models.Broker.Requests.Skill;
using LT.DigitalOffice.Models.Broker.Responses.Skill;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Models.Db;
using MassTransit;

namespace LT.DigitalOffice.SkillService.Broker.Consumers
{
  public class GetUserSkillsConsumer : IConsumer<IGetUserSkillsRequest>
  {
    private readonly IUserSkillRepository _repository;

    private async Task<List<UserSkillData>> GetUserSkillsAsync(IGetUserSkillsRequest request)
    {
      List<DbUserSkill> userSkills = await _repository.GetAsync(request.UserId);

      return userSkills?.Select(
          us => new UserSkillData(
            us.Skill.Id,
            us.Skill.Name))
        .ToList();
    }

    public GetUserSkillsConsumer(IUserSkillRepository repository)
    {
      _repository = repository;
    }

    public async Task Consume(ConsumeContext<IGetUserSkillsRequest> context)
    {
      List<UserSkillData> userSkills = await GetUserSkillsAsync(context.Message);

      await context.RespondAsync<IOperationResult<IGetUserSkillsResponse>>(
        OperationResultWrapper.CreateResponse(_ => IGetUserSkillsResponse.CreateObj(userSkills), context));
    }
  }
}

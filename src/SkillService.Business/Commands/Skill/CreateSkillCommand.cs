using System;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Business.Commands.Skill.Interfaces;
using LT.DigitalOffice.SkillService.Data.Interfaces;

namespace LT.DigitalOffice.SkillService.Business.Commands.Skill
{
  public class CreateSkillCommand : ICreateSkillCommand
  {
    private readonly ISkillRepository _repository;
    private readonly IResponseCreator _responseCreator;

    public CreateSkillCommand(
      ISkillRepository repository,
      IResponseCreator responseCreator)
    {
      _repository = repository;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(string name)
    {
      if (name is null || String.IsNullOrEmpty(name.Trim()) || name.Length > 100)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      };

      OperationResultResponse<Guid?> response = new();

      response.Body = await _repository.CreateAsync(name);
      response.Status = OperationResultStatusType.FullSuccess;

      if (response.Body is null)
      {
        response = _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      }

      return response;
    }
  }
}

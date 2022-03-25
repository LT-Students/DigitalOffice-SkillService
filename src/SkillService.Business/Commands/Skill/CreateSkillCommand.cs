using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Business.Commands.Skill.Interfaces;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Validation.Interfaces;

namespace LT.DigitalOffice.SkillService.Business.Commands.Skill
{
  public class CreateSkillCommand : ICreateSkillCommand
  {
    private readonly ISkillRepository _repository;
    private readonly IResponseCreator _responseCreator;
    private readonly ICreateSkillValidator _validator;

    public CreateSkillCommand(
      ISkillRepository repository,
      IResponseCreator responseCreator,
      ICreateSkillValidator validator)
    {
      _repository = repository;
      _responseCreator = responseCreator;
      _validator = validator;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(string name)
    {
      ValidationResult validationResult = await _validator.ValidateAsync(name);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      OperationResultResponse<Guid?> response = new();

      response.Body = await _repository.CreateAsync(name.Trim().ToLower());
      response.Status = OperationResultStatusType.FullSuccess;

      if (response.Body is null)
      {
        response = _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      }

      return response;
    }
  }
}

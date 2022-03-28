using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Business.Commands.Skill.Interfaces;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Mappers.Db.Interfsaces;
using LT.DigitalOffice.SkillService.Validation.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.SkillService.Business.Commands.Skill
{
  public class CreateSkillCommand : ICreateSkillCommand
  {
    private readonly ISkillRepository _repository;
    private readonly IResponseCreator _responseCreator;
    private readonly ICreateSkillValidator _validator;
    private readonly IDbSkillMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateSkillCommand(
      ISkillRepository repository,
      IResponseCreator responseCreator,
      ICreateSkillValidator validator,
      IDbSkillMapper mapper,
      IHttpContextAccessor httpContextAccessor)
    {
      _repository = repository;
      _responseCreator = responseCreator;
      _validator = validator;
      _mapper = mapper;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(string name)
    {
      if (!_validator.ValidateCustom(name, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest, errors);
      }

      OperationResultResponse<Guid?> response = new();

      response.Body = await _repository.CreateAsync(_mapper.Map(name));

      if (response.Body is null)
      {
        response = _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      }

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}

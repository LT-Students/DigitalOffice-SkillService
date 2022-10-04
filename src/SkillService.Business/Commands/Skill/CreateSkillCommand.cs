using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Business.Commands.Skill.Interfaces;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Mappers.Db.Interfaces;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;
using LT.DigitalOffice.SkillService.Validation.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.SkillService.Business.Commands.Skill
{
  public class CreateSkillCommand : ICreateSkillCommand
  {
    private readonly ISkillRepository _repository;
    private readonly IResponseCreator _responseCreator;
    private readonly ICreateSkillRequestValidator _validator;
    private readonly IDbSkillMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateSkillCommand(
      ISkillRepository repository,
      IResponseCreator responseCreator,
      ICreateSkillRequestValidator validator,
      IDbSkillMapper mapper,
      IHttpContextAccessor httpContextAccessor)
    {
      _repository = repository;
      _responseCreator = responseCreator;
      _validator = validator;
      _mapper = mapper;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateSkillRequest request)
    {
      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      OperationResultResponse<Guid?> response = new();

      response.Body = await _repository.CreateAsync(_mapper.Map(request));
      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      if (response.Body is null)
      {
        response = _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      }

      return response;
    }
  }
}

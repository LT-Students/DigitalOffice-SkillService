using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.SkillService.Business.Commands.UserSkill.Interfaces;
using LT.DigitalOffice.SkillService.Data.Interfaces;
using LT.DigitalOffice.SkillService.Models.Dto.Requests;
using LT.DigitalOffice.SkillService.Validation.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.SkillService.Business.Commands.UserSkill
{
  public class EditUserSkillCommand : IEditUserSkillCommand
  {
    private readonly IUserSkillRepository _userSkillRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IEditUserSkillValidator _validator;

    public EditUserSkillCommand(
      IUserSkillRepository userSkillRepository,
      ISkillRepository skillRepository,
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      IEditUserSkillValidator validator)
    {
      _userSkillRepository = userSkillRepository;
      _skillRepository = skillRepository;
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
      _validator = validator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid userId, EditUserSkillRequest request)
    {
      if (userId != _httpContextAccessor.HttpContext.GetUserId()
        && !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveUsers))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<bool>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      OperationResultResponse<bool> response = new();

      response.Body = await _userSkillRepository.EditAsync(userId, request);

      if (!response.Body)
      {
        response = _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
      }

      await _skillRepository.RemoveUnusedSkillsAsync();

      return response;
    }
  }
}

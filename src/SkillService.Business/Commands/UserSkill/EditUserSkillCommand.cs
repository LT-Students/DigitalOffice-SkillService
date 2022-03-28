﻿using System;
using System.Collections.Generic;
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
using LT.DigitalOffice.SkillService.Mappers.Db.Interfsaces;
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
    private readonly IDbUserSkillMapper _mapper;

    public EditUserSkillCommand(
      IUserSkillRepository userSkillRepository,
      ISkillRepository skillRepository,
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      IEditUserSkillValidator validator,
      IDbUserSkillMapper mapper)
    {
      _userSkillRepository = userSkillRepository;
      _skillRepository = skillRepository;
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
      _validator = validator;
      _mapper = mapper;
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

      List<Guid> existSkills = await _userSkillRepository.GetUserSkillAsync(userId);
      List<Guid> conflictSkills = request.SkillsToAdd.Intersect(request.SkillsToRemove).ToList();

      request.SkillsToAdd = request.SkillsToAdd.Except(conflictSkills).ToList();
      request.SkillsToAdd = request.SkillsToAdd.Except(existSkills).ToList();

      request.SkillsToRemove = request.SkillsToRemove.Except(conflictSkills).ToList();
      request.SkillsToRemove = request.SkillsToRemove.Intersect(existSkills).ToList();

      if (request.SkillsToAdd.Any())
      {
        await _userSkillRepository.AddUserSkillAsync(_mapper.Map(userId, request.SkillsToAdd));
        await _skillRepository
            .UpgradeTotalCountAsync(request.SkillsToAdd);
      }

      if (request.SkillsToRemove.Any())
      {
        await _userSkillRepository.RemoveUserSkillAsync(userId, request.SkillsToRemove);
        await _skillRepository
          .DowngradeTotalCountAsync(request.SkillsToRemove);
      }

      response.Body = true;

      await _skillRepository.RemoveUnusedSkillsAsync();

      return response;
    }
  }
}

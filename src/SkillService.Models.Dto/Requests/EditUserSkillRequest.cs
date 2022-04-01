using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.SkillService.Models.Dto.Requests
{
  public class EditUserSkillRequest
  {
    public List<Guid> SkillsToAdd { get; set; }
    public List<Guid> SkillsToRemove { get; set; }
  }
}

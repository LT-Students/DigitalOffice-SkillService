using System;

namespace LT.DigitalOffice.SkillService.Models.Dto.Models
{
  public record SkillInfo
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int TotalCount { get; set; }
  }
}

using LT.DigitalOffice.Kernel.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.SkillService.Models.Dto.Requests.Filters
{
  public record FindSkillFilter : BaseFindFilter
  {
    [FromQuery(Name = "nameincludesubstring")]
    public string NameIncludeSubstring { get; set; }

    [FromQuery(Name = "ascendingsort")]
    public bool? AscendingSort { get; set; }

  }
}

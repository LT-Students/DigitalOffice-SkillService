using LT.DigitalOffice.Kernel.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.SkillService.Models.Dto.Requests.Filters
{
  public record FindSkillFilter : BaseFindFilter
  {
    [FromQuery(Name = "nameIncludeSubstring")]
    public string NameIncludeSubstring { get; set; }

    [FromQuery(Name = "isAscendingSort")]
    public bool? IsAscendingSort { get; set; }

  }
}

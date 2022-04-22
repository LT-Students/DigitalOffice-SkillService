using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SkillService.Models.Db;
using LT.DigitalOffice.SkillService.Models.Dto.Models;

namespace LT.DigitalOffice.SkillService.Mappers.Db.Interfsaces
{
  [AutoInject]
  public interface ISkillInfoMapper
  {
    SkillInfo Map(DbSkill dbSkill);
  }
}

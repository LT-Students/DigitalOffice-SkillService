using LT.DigitalOffice.SkillService.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.SkillService.Data.Provider.MsSql.Ef.Configurations
{
  public class DbUserSkillConfiguration : IEntityTypeConfiguration<DbUserSkill>
  {
    public void Configure(EntityTypeBuilder<DbUserSkill> builder)
    {
      builder
        .ToTable(DbUserSkill.TableName);

      builder
        .HasKey(us => us.Id);

      builder
        .HasOne(us => us.Skill)
        .WithMany(s => s.UsersSkills);
    }
  }
}

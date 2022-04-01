using LT.DigitalOffice.SkillService.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.SkillService.Data.Provider.MsSql.Ef.Configurations
{
  public class DbSkillConfiguration : IEntityTypeConfiguration<DbSkill>
  {
    public void Configure(EntityTypeBuilder<DbSkill> builder)
    {
      builder
        .ToTable(DbSkill.TableName);

      builder
        .HasKey(s => s.Id);

      builder
        .Property(s => s.Name)
        .IsRequired();

      builder
        .HasMany(s => s.UsersSkills)
        .WithOne(us => us.Skill);
    }
  }
}

using System.Reflection;
using System.Threading.Tasks;
using LT.DigitalOffice.SkillService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.SkillService.Data.Provider.MsSql.Ef
{
  public class SkillServiceDbContext : DbContext, IDataProvider
  {
    public DbSet<DbSkill> Skills { get; set; }
    public DbSet<DbUserSkill> UsersSkills { get; set; }

    public SkillServiceDbContext(DbContextOptions<SkillServiceDbContext> options)
    : base(options)
    {
    }

    // Fluent API is written here.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("LT.DigitalOffice.SkillService.Models.Db"));
    }

    public void Save()
    {
      SaveChanges();
    }

    public object MakeEntityDetached(object obj)
    {
      Entry(obj).State = EntityState.Detached;

      return Entry(obj).State;
    }

    public void EnsureDeleted()
    {
      Database.EnsureDeleted();
    }

    public bool IsInMemory()
    {
      return Database.IsInMemory();
    }

    public async Task SaveAsync()
    {
      await SaveChangesAsync();
    }
  }
}

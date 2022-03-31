using System;
using LT.DigitalOffice.SkillService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.SkillService.Data.Provider.MsSql.Ef.Migrations
{
  [DbContext(typeof(SkillServiceDbContext))]
  [Migration("20220309150000_InitialTable")]
  public class InitialTable : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: DbSkill.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          BecameUnusedAtUtc = table.Column<DateTime>(nullable: true),
          TotalCount = table.Column<int>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbSkill.TableName}", x => x.Id);
        });

      migrationBuilder.CreateTable(
        name: DbUserSkill.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          UserId = table.Column<Guid>(nullable: false),
          SkillId = table.Column<Guid>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbUserSkill.TableName}", x => x.Id);
        });
    }

    protected override void Down(MigrationBuilder builder)
    {
      builder.DropTable(
        name: DbSkill.TableName);

      builder.DropTable(
        name: DbUserSkill.TableName);
    }
  }
}

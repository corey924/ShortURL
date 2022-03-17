using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShortURL.Services.Migrations
{
  public partial class CreatedDB : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "ActionLogs",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Device = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Input = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
            Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
            User = table.Column<string>(type: "nvarchar(max)", nullable: true),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_ActionLogs", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "ApiAuthorize",
          columns: table => new
          {
            Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Enable = table.Column<bool>(type: "bit", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_ApiAuthorize", x => x.Token);
          });

      migrationBuilder.CreateTable(
          name: "UrlRedirection",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
            Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Enable = table.Column<bool>(type: "bit", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_UrlRedirection", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "UrlRedirectionLogs",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
            ToUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
            IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
            DeviceInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_UrlRedirectionLogs", x => x.Id);
          });

      migrationBuilder.CreateIndex(
          name: "IX_UrlRedirection_Code",
          table: "UrlRedirection",
          column: "Code",
          unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "ActionLogs");

      migrationBuilder.DropTable(
          name: "ApiAuthorize");

      migrationBuilder.DropTable(
          name: "UrlRedirection");

      migrationBuilder.DropTable(
          name: "UrlRedirectionLogs");
    }
  }
}

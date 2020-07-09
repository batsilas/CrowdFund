using Microsoft.EntityFrameworkCore.Migrations;

namespace CrowFund.Core.Migrations
{
    public partial class addIdUserBacker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBacker",
                table: "UserBacker");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserBacker",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserBacker",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBacker",
                table: "UserBacker",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBacker",
                table: "UserBacker");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserBacker");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserBacker",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBacker",
                table: "UserBacker",
                columns: new[] { "ProjectId", "UserId" });
        }
    }
}

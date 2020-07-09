using Microsoft.EntityFrameworkCore.Migrations;

namespace CrowFund.Core.Migrations
{
    public partial class addFundsReceivedCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FundsReceivedCounter",
                table: "Project",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FundsReceivedCounter",
                table: "Project");
        }
    }
}

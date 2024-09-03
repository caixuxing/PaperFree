using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trasen.PaperFree.Infrastructure.Migrations
{
    public partial class rm_ProcessNode_REJECT_NODE_ID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "REJECT_NODE_ID",
                table: "PROCESS_NODE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "REJECT_NODE_ID",
                table: "PROCESS_NODE",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: true,
                comment: "驳回节点");
        }
    }
}

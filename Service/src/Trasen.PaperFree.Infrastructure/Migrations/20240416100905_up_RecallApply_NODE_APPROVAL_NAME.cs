using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trasen.PaperFree.Infrastructure.Migrations
{
    public partial class up_RecallApply_NODE_APPROVAL_NAME : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NODE_APPROVAL_NAME",
                table: "RECALL_APPLY",
                type: "NVARCHAR2(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "当前节点审批人名称",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "当前节点审批人名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NODE_APPROVAL_NAME",
                table: "RECALL_APPLY",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: true,
                comment: "当前节点审批人名称",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(1000)",
                oldMaxLength: 1000,
                oldNullable: true,
                oldComment: "当前节点审批人名称");
        }
    }
}

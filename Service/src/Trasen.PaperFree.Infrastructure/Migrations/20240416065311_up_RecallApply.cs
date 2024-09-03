using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trasen.PaperFree.Infrastructure.Migrations
{
    public partial class up_RecallApply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ATTACHMENT_MATERIALS",
                table: "RECALL_APPLY",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: true,
                comment: "附件材料",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(200)",
                oldMaxLength: 200,
                oldComment: "附件材料");

            migrationBuilder.AlterColumn<string>(
                name: "APPLY_REASON",
                table: "RECALL_APPLY",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: true,
                comment: "申请原因",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(200)",
                oldMaxLength: 200,
                oldComment: "申请原因");

            migrationBuilder.AlterColumn<string>(
                name: "APPLY_NAME",
                table: "RECALL_APPLY",
                type: "NVARCHAR2(30)",
                maxLength: 30,
                nullable: true,
                comment: "申请名称",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(30)",
                oldMaxLength: 30,
                oldComment: "申请名称");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ATTACHMENT_MATERIALS",
                table: "RECALL_APPLY",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "附件材料",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "附件材料");

            migrationBuilder.AlterColumn<string>(
                name: "APPLY_REASON",
                table: "RECALL_APPLY",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                comment: "申请原因",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "申请原因");

            migrationBuilder.AlterColumn<string>(
                name: "APPLY_NAME",
                table: "RECALL_APPLY",
                type: "NVARCHAR2(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                comment: "申请名称",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "申请名称");
        }
    }
}

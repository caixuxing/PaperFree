using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trasen.PaperFree.Infrastructure.Migrations
{
    public partial class up_oederyID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SECRET_LEVEL",
                table: "ARCHIVER_MEUM",
                type: "NVARCHAR2(4)",
                maxLength: 4,
                nullable: true,
                comment: "保密等级",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(4)",
                oldMaxLength: 4,
                oldComment: "保密等级");

            migrationBuilder.AlterColumn<int>(
                name: "ORDERBY",
                table: "ARCHIVER_MEUM",
                type: "NUMBER(10)",
                nullable: false,
                comment: "排序",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldComment: "排序");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SECRET_LEVEL",
                table: "ARCHIVER_MEUM",
                type: "NVARCHAR2(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "",
                comment: "保密等级",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(4)",
                oldMaxLength: 4,
                oldNullable: true,
                oldComment: "保密等级");

            migrationBuilder.AlterColumn<string>(
                name: "ORDERBY",
                table: "ARCHIVER_MEUM",
                type: "NVARCHAR2(2000)",
                nullable: false,
                comment: "排序",
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldComment: "排序");
        }
    }
}

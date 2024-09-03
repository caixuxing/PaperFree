using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trasen.PaperFree.Infrastructure.Migrations
{
    public partial class up_Watermarkadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FONTSIZE",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                comment: "字体大小");

            migrationBuilder.AddColumn<string>(
                name: "GAPX",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                comment: "左右间隔");

            migrationBuilder.AddColumn<string>(
                name: "GAPY",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                comment: "上下间隔");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FONTSIZE",
                table: "BASE_WATERMARK");

            migrationBuilder.DropColumn(
                name: "GAPX",
                table: "BASE_WATERMARK");

            migrationBuilder.DropColumn(
                name: "GAPY",
                table: "BASE_WATERMARK");
        }
    }
}

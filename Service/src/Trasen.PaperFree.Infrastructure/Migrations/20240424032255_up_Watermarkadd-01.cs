using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trasen.PaperFree.Infrastructure.Migrations
{
    public partial class up_Watermarkadd01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WATERMARK_NAME",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: true,
                comment: "水印名称",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "水印名称");

            migrationBuilder.AlterColumn<string>(
                name: "COLOR",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: false,
                comment: "颜色",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20,
                oldComment: "颜色");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WATERMARK_NAME",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: true,
                comment: "水印名称",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "水印名称");

            migrationBuilder.AlterColumn<string>(
                name: "COLOR",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: false,
                comment: "颜色",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(100)",
                oldMaxLength: 100,
                oldComment: "颜色");
        }
    }
}

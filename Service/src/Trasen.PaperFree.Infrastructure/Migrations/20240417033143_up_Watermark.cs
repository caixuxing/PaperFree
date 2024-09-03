using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trasen.PaperFree.Infrastructure.Migrations
{
    public partial class up_Watermark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YSTATION",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                comment: "y坐标",
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldMaxLength: 10,
                oldComment: "y坐标");

            migrationBuilder.AlterColumn<string>(
                name: "XSTATION",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                comment: "x坐标",
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldMaxLength: 10,
                oldComment: "x坐标");

            migrationBuilder.AlterColumn<string>(
                name: "WIDTH",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                comment: "宽度",
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldMaxLength: 10,
                oldComment: "宽度");

            migrationBuilder.AlterColumn<string>(
                name: "HIGHT",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                comment: "高度",
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldMaxLength: 10,
                oldComment: "高度");

            migrationBuilder.AlterColumn<string>(
                name: "ANGLE",
                table: "BASE_WATERMARK",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                comment: "角度",
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldMaxLength: 10,
                oldComment: "角度");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "YSTATION",
                table: "BASE_WATERMARK",
                type: "NUMBER(10)",
                maxLength: 10,
                nullable: false,
                comment: "y坐标",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(10)",
                oldMaxLength: 10,
                oldComment: "y坐标");

            migrationBuilder.AlterColumn<int>(
                name: "XSTATION",
                table: "BASE_WATERMARK",
                type: "NUMBER(10)",
                maxLength: 10,
                nullable: false,
                comment: "x坐标",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(10)",
                oldMaxLength: 10,
                oldComment: "x坐标");

            migrationBuilder.AlterColumn<int>(
                name: "WIDTH",
                table: "BASE_WATERMARK",
                type: "NUMBER(10)",
                maxLength: 10,
                nullable: false,
                comment: "宽度",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(10)",
                oldMaxLength: 10,
                oldComment: "宽度");

            migrationBuilder.AlterColumn<int>(
                name: "HIGHT",
                table: "BASE_WATERMARK",
                type: "NUMBER(10)",
                maxLength: 10,
                nullable: false,
                comment: "高度",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(10)",
                oldMaxLength: 10,
                oldComment: "高度");

            migrationBuilder.AlterColumn<int>(
                name: "ANGLE",
                table: "BASE_WATERMARK",
                type: "NUMBER(10)",
                maxLength: 10,
                nullable: false,
                comment: "角度",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(10)",
                oldMaxLength: 10,
                oldComment: "角度");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trasen.PaperFree.Infrastructure.Migrations
{
    public partial class up_ignoreitme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IGNORE_ITME",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    ARCHIVE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "档案号"),
                    MEUM_TREE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "必传文件配置ID"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IGNORE_ITME", x => x.ID);
                },
                comment: "科室档案目录必填项忽略");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IGNORE_ITME");
        }
    }
}

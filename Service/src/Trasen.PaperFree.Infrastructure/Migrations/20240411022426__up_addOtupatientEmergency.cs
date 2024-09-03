using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trasen.PaperFree.Infrastructure.Migrations
{
    public partial class _up_addOtupatientEmergency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OUTPATIENT_EMERGENCY",
                columns: table => new
                {
                    ARCHIVEID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "档案号"),
                    HOSP_RECORD_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "就诊号"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "机构编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "患者姓名"),
                    SEXTYPE = table.Column<int>(type: "NUMBER(10)", maxLength: 50, nullable: false, comment: "患者性别"),
                    DATE_OF_BIRTH = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "出生日期"),
                    AGE = table.Column<int>(type: "NUMBER(10)", nullable: true, comment: "患者年龄"),
                    ID_CARD = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "身份证号码"),
                    SEE_PATIENTS_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "接诊时间"),
                    SEEDEPTCODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "接诊科室"),
                    RECEIVE_DOCTOR_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "接诊医生"),
                    ICD_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "门诊诊断编码"),
                    ICD_NAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "门诊诊断名称")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OUTPATIENT_EMERGENCY", x => x.ARCHIVEID);
                },
                comment: "归档菜单目录");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OUTPATIENT_EMERGENCY");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trasen.PaperFree.Infrastructure.Migrations
{
    public partial class up_inpatientInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FILE_FLAG",
                table: "INPATIENT_INFO");

            migrationBuilder.DropColumn(
                name: "GET_DATE",
                table: "INPATIENT_INFO");

            migrationBuilder.DropColumn(
                name: "HospName",
                table: "INPATIENT_INFO");

            migrationBuilder.DropColumn(
                name: "OrgName",
                table: "INPATIENT_INFO");

            migrationBuilder.DropColumn(
                name: "PmrfType",
                table: "INPATIENT_INFO");

            migrationBuilder.DropColumn(
                name: "SIGN_STATUS",
                table: "INPATIENT_INFO");

            migrationBuilder.RenameColumn(
                name: "HospRecordId",
                table: "INPATIENT_INFO",
                newName: "HOSP_RECORD_ID");

            migrationBuilder.AlterColumn<string>(
                name: "SEX_TYPE",
                table: "OUTPATIENT_INFO",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                comment: "性别",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(4)",
                oldMaxLength: 4,
                oldComment: "性别");

            migrationBuilder.AlterColumn<string>(
                name: "SEX_TYPE",
                table: "INPATIENT_INFO",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                comment: "性别",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(4)",
                oldMaxLength: 4,
                oldComment: "性别");

            migrationBuilder.AlterColumn<string>(
                name: "HOSP_RECORD_ID",
                table: "INPATIENT_INFO",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                comment: "档案号（主键）",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HOSP_RECORD_ID",
                table: "INPATIENT_INFO",
                newName: "HospRecordId");

            migrationBuilder.AlterColumn<string>(
                name: "SEX_TYPE",
                table: "OUTPATIENT_INFO",
                type: "NVARCHAR2(4)",
                maxLength: 4,
                nullable: false,
                comment: "性别",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(10)",
                oldMaxLength: 10,
                oldComment: "性别");

            migrationBuilder.AlterColumn<string>(
                name: "SEX_TYPE",
                table: "INPATIENT_INFO",
                type: "NVARCHAR2(4)",
                maxLength: 4,
                nullable: false,
                comment: "性别",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(10)",
                oldMaxLength: 10,
                oldComment: "性别");

            migrationBuilder.AlterColumn<string>(
                name: "HospRecordId",
                table: "INPATIENT_INFO",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(50)",
                oldMaxLength: 50,
                oldComment: "档案号（主键）");

            migrationBuilder.AddColumn<string>(
                name: "FILE_FLAG",
                table: "INPATIENT_INFO",
                type: "NVARCHAR2(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "",
                comment: "文件采集标志（0未采集，1已采集）");

            migrationBuilder.AddColumn<DateTime>(
                name: "GET_DATE",
                table: "INPATIENT_INFO",
                type: "TIMESTAMP(7)",
                nullable: true,
                comment: "采集时间");

            migrationBuilder.AddColumn<string>(
                name: "HospName",
                table: "INPATIENT_INFO",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrgName",
                table: "INPATIENT_INFO",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PmrfType",
                table: "INPATIENT_INFO",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SIGN_STATUS",
                table: "INPATIENT_INFO",
                type: "NVARCHAR2(2)",
                maxLength: 2,
                nullable: true,
                comment: "签名状态0、待签名；1、签名中；2、已签名");
        }
    }
}

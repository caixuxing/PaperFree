using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trasen.PaperFree.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ANNOTATION_TABLE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    ARCHIVE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "档案号"),
                    FILE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "文件id"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "机构编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    ANNOTATION = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true, comment: "批注内容"),
                    DEPT_CODE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "批注科室编码"),
                    REMARK = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true, comment: "备注"),
                    LOWER = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "评分 "),
                    AN_NOT_ATION_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "批注日期"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANNOTATION_TABLE", x => x.ID);
                },
                comment: "批注表");

            migrationBuilder.CreateTable(
                name: "ARCHIVE_APPLY",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    ARCHIVAL_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "档案号"),
                    ARCHIVE_NAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "申请名称,默认为当前病案人归档作为名称，批量为批量归档申请"),
                    CURRENT_STATUS = table.Column<int>(type: "NUMBER(10)", maxLength: 10, nullable: false, comment: "状态 流程状态：开始》审批中》结束 、作废"),
                    IS_END = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "流程是否结束 状态：是 、否"),
                    PROCESS_DESIGN_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "流程模板ID 绑定当前当前申请审批模板"),
                    CURRENT_APPROVAL_NODE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "当前审批节点ID "),
                    CURRENT_APPROVAL_NODE_NAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "当前审批节点名称"),
                    NODE_APPROVA_NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "当前节点审批人名称"),
                    APPLY_PERSON_NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "申请人姓名"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "机构编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARCHIVE_APPLY", x => x.ID);
                },
                comment: "归档申请");

            migrationBuilder.CreateTable(
                name: "ARCHIVER_MEUM",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    MENU_NAME = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false, comment: "目录名称"),
                    PARENT_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "上级目录ID"),
                    PERMISSION = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "根据权限设置医护所能操作的目录"),
                    MEUM_TYPE = table.Column<string>(type: "NVARCHAR2(4)", maxLength: 4, nullable: true, comment: "目录类型（1、目录，2、节点）"),
                    ORDERBY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false, comment: "排序"),
                    SECRET_LEVEL = table.Column<string>(type: "NVARCHAR2(4)", maxLength: 4, nullable: false, comment: "保密等级"),
                    IS_HIGHSHOTS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false, comment: "是否高拍"),
                    IS_SIGNATURE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false, comment: "是否签名"),
                    IS_ALLORG = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false, comment: "是否适用所有机构（0、否；1、是）"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "机构编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARCHIVER_MEUM", x => x.ID);
                },
                comment: "归档菜单目录");

            migrationBuilder.CreateTable(
                name: "BASE_BORROW_MODE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    MODE_NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "模板名称"),
                    DEPT_CODE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "科室编码"),
                    USER_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "用户编码"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    InputCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IS_ENABLE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "是否启用"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASE_BORROW_MODE", x => x.ID);
                },
                comment: "借阅模板设置");

            migrationBuilder.CreateTable(
                name: "BASE_WATERMARK",
                columns: table => new
                {
                    WATERMARK_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    WATERMARK_NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "水印名称"),
                    USE_SCENE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "使用场景"),
                    COLOR = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "颜色"),
                    BIG = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "大小"),
                    XSTATION = table.Column<int>(type: "NUMBER(10)", maxLength: 10, nullable: false, comment: "x坐标"),
                    YSTATION = table.Column<int>(type: "NUMBER(10)", maxLength: 10, nullable: false, comment: "y坐标"),
                    ANGLE = table.Column<int>(type: "NUMBER(10)", maxLength: 10, nullable: false, comment: "角度"),
                    STYLE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "样式"),
                    DIRECTION = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "方向"),
                    FONT = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "字体"),
                    PELLUCIDITY = table.Column<int>(type: "NUMBER(10)", maxLength: 10, nullable: true, comment: "透明度"),
                    HIGHT = table.Column<int>(type: "NUMBER(10)", maxLength: 10, nullable: false, comment: "高度"),
                    WIDTH = table.Column<int>(type: "NUMBER(10)", maxLength: 10, nullable: false, comment: "宽度"),
                    IS_SUITABLE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "是否合适大小"),
                    PICTURE = table.Column<string>(type: "CLOB", nullable: true, comment: "图片"),
                    PICX = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "PicX"),
                    PICY = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "PicY"),
                    STATUS = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "状态"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "使用机构"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    INPUT_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "辖区编码"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BASE_WATERMARK", x => x.WATERMARK_ID);
                },
                comment: "水印表");

            migrationBuilder.CreateTable(
                name: "CASE_SHELF_MANAGEMENT",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    WAREHOUSE_NUMBER = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "仓库编号"),
                    WAREHOUSE_NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "仓库名称"),
                    SHELF_NO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "病架号"),
                    STORAGE_NUMBER_SEGMENT = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "存储号段"),
                    LINE_NUMBER = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "行数"),
                    NUMBER_OLUMNS = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "列数"),
                    STATUS = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "状态"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "机构编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    INPUT_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "辖区编码"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CASE_SHELF_MANAGEMENT", x => x.ID);
                },
                comment: "纸质病例存储管理");

            migrationBuilder.CreateTable(
                name: "DEPT_MEUM_TREE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    DEPT_ID = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "科室ID"),
                    ARCHIVER_MEUM_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "归档目录模板ID"),
                    PARENT_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "上级目录ID"),
                    IS_REQUIRED = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "是否必填"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "使用机构"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    INPUT_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "辖区编码"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPT_MEUM_TREE", x => x.ID);
                },
                comment: "必传文件配置");

            migrationBuilder.CreateTable(
                name: "ESSENTIAL_DOCUMENTS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    DEPT_CODE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "科室编码"),
                    FATHER_MEUMID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "目录编码"),
                    MEUM_TYPE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "目录类型"),
                    STATUS = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "状态"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "使用机构"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    INPUT_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "辖区编码"),
                    ORDERID = table.Column<int>(type: "NUMBER(10)", maxLength: 10, nullable: true, comment: "排序"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESSENTIAL_DOCUMENTS", x => x.ID);
                },
                comment: "必传文件配置");

            migrationBuilder.CreateTable(
                name: "FILES_HIS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    ARCHIVE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "档案号ID"),
                    MEUM_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "病历目录表ID"),
                    FILE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "文件唯-ID"),
                    FILE_TRUENAME = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true, comment: "文件原名称"),
                    FILE_SAVENAME = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true, comment: "文件保存后名称"),
                    FILE_TYPE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "文件类型格式"),
                    FILE_PATH = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false, comment: "文件存储路径"),
                    FILE_SIZE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "文件大小"),
                    ORDER_NO = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "排序号"),
                    STATUS = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "状态【物理文件上传状态】"),
                    STORAGE_STATE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "文件存储状态【临时、正式】"),
                    DATA_TYPE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "数据类型 1 门诊 2 住院"),
                    REMARK = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true, comment: "备注"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FILES_HIS", x => x.ID);
                },
                comment: "HIS文件表");

            migrationBuilder.CreateTable(
                name: "FILES_OTHER",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    ARCHIVE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "档案号ID"),
                    MEUM_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "病历目录表ID"),
                    FILE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "文件唯-ID"),
                    FILE_TRUENAME = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true, comment: "文件原名称"),
                    FILE_SAVENAME = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true, comment: "文件保存后名称"),
                    FILE_TYPE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "文件类型格式"),
                    FILE_PATH = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true, comment: "文件存储路径"),
                    FILE_SIZE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "文件大小"),
                    ORDER_NO = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "排序号"),
                    STATUS = table.Column<int>(type: "NUMBER(10)", maxLength: 10, nullable: false, comment: "状态【物理文件上传状态】"),
                    STORAGE_STATE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "文件存储状态【临时、正式】"),
                    REMARK = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true, comment: "备注"),
                    SOURCE_CODE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "文件来源于哪个系统"),
                    FILE_CATEGORY_CODE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true, comment: "文件分类编码"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FILES_OTHER", x => x.ID);
                },
                comment: "其他文件");

            migrationBuilder.CreateTable(
                name: "INPATIENT_INFO",
                columns: table => new
                {
                    ARCHIVE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "档案号（主键）"),
                    HospRecordId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ADMISS_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "住院号"),
                    INPATIENT_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "住院ID"),
                    VISIT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false, comment: "住院次数"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "机构编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "院区编码"),
                    INPUT_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "辖区编码"),
                    HospName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    OrgName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    NAME = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "姓名"),
                    SEX_TYPE = table.Column<string>(type: "NVARCHAR2(4)", maxLength: 4, nullable: false, comment: "性别"),
                    DATE_OF_BIRTH = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "出生日期"),
                    AGE = table.Column<int>(type: "NUMBER(10)", maxLength: 3, nullable: true, comment: "年龄"),
                    ID_CARD = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: true, comment: "身份证号"),
                    ENTER_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "入院时间"),
                    ENTER_DEPT = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "入院科室"),
                    ENTER_WARD_CODE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "病区编码"),
                    DOCTOR_ZYYS_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "住院医生编号"),
                    DOCTOR_ZZYS_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "主治医生编号"),
                    DOCTOR_KZR_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "科主任编号"),
                    CHARGE_NURSE_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "责任护士编号"),
                    GET_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "采集时间"),
                    PmrfType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SIGN_STATUS = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: true, comment: "签名状态0、待签名；1、签名中；2、已签名"),
                    FILE_FLAG = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false, comment: "文件采集标志（0未采集，1已采集）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INPATIENT_INFO", x => x.ARCHIVE_ID);
                },
                comment: "在院病人就诊信息表");

            migrationBuilder.CreateTable(
                name: "MedicalRecor",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    MedicalRecorNo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MedicalRecorTitle = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MedicalRecorStatus = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatorId = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CreationTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    LastModifyTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    LastModifyId = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    RowVersion = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IsDeleted = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecor", x => x.ID);
                },
                comment: "病案");

            migrationBuilder.CreateTable(
                name: "OUTPATIENT_INFO",
                columns: table => new
                {
                    ARCHIVE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "档案号（主键）"),
                    HOSP_RECORD_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "就诊号"),
                    PATIENT_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "病案号"),
                    ADMISS_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "住院号"),
                    INPATIENT_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "住院ID"),
                    VISIT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false, comment: "住院次数"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "机构编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "院区编码"),
                    INPUT_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "辖区编码"),
                    NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "姓名"),
                    SEX_TYPE = table.Column<string>(type: "NVARCHAR2(4)", maxLength: 4, nullable: false, comment: "性别"),
                    DATE_OF_BIRTH = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "出生日期"),
                    AGE = table.Column<int>(type: "NUMBER(10)", nullable: true, comment: "年龄"),
                    ID_CARD = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: true, comment: "身份证号"),
                    ENTER_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "入院时间"),
                    OUT_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "出院时间"),
                    ENTER_DEPT_CODE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "入院科室"),
                    OUT_DEPT_CODE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "出院科室"),
                    OUT_WARD_CODE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true, comment: "病区编码"),
                    DOCTOR_KZR_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "科主任编号"),
                    DOCTOR_ZRYS_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "主任医生编号"),
                    DOCTOR_ZZYS_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "主治医生编号"),
                    DOCTOR_ZYYS_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "住院医生编号"),
                    CHARGE_NURSE_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "责任护士编号"),
                    BASY_STATUS = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "首页录入标志（编目标志）"),
                    DAYS = table.Column<int>(type: "NUMBER(10)", maxLength: 20, nullable: true, comment: "年龄(天)"),
                    IS_OVERDATE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "是否逾期"),
                    CREATE_DATE_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "创建时间"),
                    FILE_FLAG = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "文件采集标志（0未采集，1已采集）"),
                    STATUS = table.Column<int>(type: "NUMBER(10)", maxLength: 20, nullable: false, comment: "状态"),
                    CASE_TYPE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "入院方式，1.门诊  2.住院"),
                    IS_LOCK = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "是否锁定（0未锁定，1锁定，2解锁,3解锁中）")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OUTPATIENT_INFO", x => x.ARCHIVE_ID);
                },
                comment: "在院病人就诊信息表");

            migrationBuilder.CreateTable(
                name: "PAY_CONFIG",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    SERVICE_PROVIDERS = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false, comment: "服务提供者"),
                    APP_ID = table.Column<int>(type: "NUMBER(10)", maxLength: 40, nullable: false, comment: "AppID"),
                    APP_SECRET = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false, comment: "AppSecret"),
                    MERCHANT_NUMBER = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false, comment: "商户号"),
                    CALLBACK_URL = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "回调URL"),
                    INTERFACE_VERSION = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "接口版本"),
                    TOKEN = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false, comment: "Token"),
                    ENCRYPTION_KEY = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false, comment: "加密密钥"),
                    COMPLETIONNOTIFICATION = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "支付成功通知模板"),
                    IS_ENABLE = table.Column<int>(type: "NUMBER(10)", nullable: false, comment: "是否启用"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "机构编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    INPUT_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "辖区编码"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAY_CONFIG", x => x.ID);
                },
                comment: "支付配置表");

            migrationBuilder.CreateTable(
                name: "PROCESS_DESIGN",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    PROCESS_NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "流程代码值"),
                    PROCESS_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "流程名称"),
                    PROCESS_TEMP_TYPE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "流程模板类型"),
                    DEPT_CODE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "科室编码"),
                    IS_ENABLE = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "是否启用"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "机构编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROCESS_DESIGN", x => x.ID);
                },
                comment: "流程设计");

            migrationBuilder.CreateTable(
                name: "RECALL_APPLY",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    ARCHIVE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "档案号"),
                    APPLY_NAME = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false, comment: "申请名称"),
                    APPLY_REASON = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false, comment: "申请原因"),
                    ATTACHMENT_MATERIALS = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false, comment: "附件材料"),
                    IS_END = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "是否结束"),
                    PROCESS_DESIGN_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "流程模板ID"),
                    CURRENT_APPROVAL_NODE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "当前审批节点ID"),
                    CURRENT_APPROVAL_NODE_NAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "当前审批节点名称"),
                    CURRENT_STATUS = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "状态"),
                    NODE_APPROVAL_NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "当前节点审批人名称"),
                    APPLY_PERSON_NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "申请人姓名"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "机构编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "院区编码"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECALL_APPLY", x => x.ID);
                },
                comment: "召回申请");

            migrationBuilder.CreateTable(
                name: "SYS_OPER_LOG",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    TITLE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "操作模块"),
                    BUSINESS_TYPE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "业务类型"),
                    REQUEST_METHOD = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "请求方法名"),
                    REQUEST_TYPE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false, comment: "请求方式"),
                    OPERATOR_TYPE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "操作类别"),
                    OPER_NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true, comment: "操作人"),
                    REQUEST_URL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false, comment: "请求Url"),
                    OPER_IP = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "操作IP地址"),
                    OPER_ADDR = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "操作地址"),
                    REQUEST_PARAM = table.Column<string>(type: "CLOB", nullable: true, comment: "请求参数"),
                    JSON_RESULT = table.Column<string>(type: "CLOB", nullable: true, comment: "返回参数"),
                    STATUS = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "操作状态"),
                    ERROR_MSG = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true, comment: "错误消息"),
                    OPER_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "操作时间"),
                    ELAPSED = table.Column<long>(type: "NUMBER(19)", nullable: false, comment: "操作用时"),
                    ORG_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "机构编码"),
                    HOSP_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "院区编码"),
                    INPUT_CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "辖区编码"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_OPER_LOG", x => x.ID);
                },
                comment: "系统操作日志");

            migrationBuilder.CreateTable(
                name: "ARCHIVE_APPROVER",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    ARCHIVE_APPLY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "归档申请ID"),
                    Work_Flow_Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "流程状态"),
                    APPROVAL_RESULT = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "审批结果"),
                    APPROVAL_REMARK = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: true, comment: "审批备注"),
                    APPROVAL_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "审批人ID"),
                    APPROVAL_DATE_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "审批时间"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARCHIVE_APPROVER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ARCHIVE_APPLY_ID",
                        column: x => x.ARCHIVE_APPLY_ID,
                        principalTable: "ARCHIVE_APPLY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "归档审批记录表");

            migrationBuilder.CreateTable(
                name: "PROCESS_NODE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    PROCESS_DESIGN_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "流程主键ID"),
                    NODE_NAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "节点名称"),
                    NODE_CODE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "节点代码值"),
                    UPPER_NODE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "上级节点"),
                    LOWER_NODE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "下级节点"),
                    NODE_MAP_WORKFLOW_STATUS = table.Column<int>(type: "NUMBER(10)", nullable: false, comment: "节点对应流程状态"),
                    EVENT_DIRECTION = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "事件方向 【拒绝、驳回、 通过】"),
                    IS_REJECT_TO_NODE = table.Column<bool>(type: "NUMBER(1)", nullable: true, comment: "是否可驳回指定节点"),
                    REJECT_NODE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "驳回节点"),
                    ODER_NO = table.Column<int>(type: "NUMBER(10)", nullable: false, comment: "排序号"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROCESS_NODE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PROCESS_DESIGN_ID",
                        column: x => x.PROCESS_DESIGN_ID,
                        principalTable: "PROCESS_DESIGN",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "流程节点");

            migrationBuilder.CreateTable(
                name: "RECALL_APPLY_AND_ARCHIVAL",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    ARCHIVAL_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "档案号"),
                    RECALL_APPLY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "召回申请ID"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECALL_APPLY_AND_ARCHIVAL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RECALL_APPLY_ID",
                        column: x => x.RECALL_APPLY_ID,
                        principalTable: "RECALL_APPLY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "召回档案申请关联表");

            migrationBuilder.CreateTable(
                name: "RECALL_APPROVER",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    RECALL_APPLY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "召回申请ID"),
                    Work_Flow_Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, comment: "流程状态"),
                    APPROVAL_RESULT = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "审批结果"),
                    APPROVAL_REMARK = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: true, comment: "审批备注"),
                    APPROVAL_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "审批人ID"),
                    APPROVAL_DATE_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "审批时间"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECALL_APPROVER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RECALLAPPROVER_ID",
                        column: x => x.RECALL_APPLY_ID,
                        principalTable: "RECALL_APPLY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "召回审批");

            migrationBuilder.CreateTable(
                name: "NODE_APPROVER",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "主键ID"),
                    PROCESS_NODE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "节点ID"),
                    APPROVER_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "审批人ID"),
                    APPROVER_ACCOUNT = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false, comment: "审批人登录账户"),
                    APPROVER_NAME = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false, comment: "审批人姓名"),
                    CREATOR_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, comment: "创建人ID"),
                    CREATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, comment: "创建时间"),
                    LAST_MODIFY_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true, comment: "最后修改时间"),
                    LAST_MODIFY_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true, comment: "更新者ID"),
                    ROW_VERSION = table.Column<byte[]>(type: "RAW(8)", rowVersion: true, nullable: false, comment: "版本号"),
                    IS_DELETED = table.Column<bool>(type: "NUMBER(1)", nullable: false, comment: "删除标识")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NODE_APPROVER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PROCESS_NODE_ID",
                        column: x => x.PROCESS_NODE_ID,
                        principalTable: "PROCESS_NODE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "节点审批人员表");

            migrationBuilder.CreateIndex(
                name: "IX_ARCHIVE_APPLY_ID",
                table: "ARCHIVE_APPROVER",
                column: "ARCHIVE_APPLY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_APPROVER_ID",
                table: "NODE_APPROVER",
                column: "PROCESS_NODE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PROCESS_DESIGN_ID",
                table: "PROCESS_NODE",
                column: "PROCESS_DESIGN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RECALL_APPLY_ID",
                table: "RECALL_APPLY_AND_ARCHIVAL",
                column: "RECALL_APPLY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RECALLAPPROVER_ID",
                table: "RECALL_APPROVER",
                column: "RECALL_APPLY_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ANNOTATION_TABLE");

            migrationBuilder.DropTable(
                name: "ARCHIVE_APPROVER");

            migrationBuilder.DropTable(
                name: "ARCHIVER_MEUM");

            migrationBuilder.DropTable(
                name: "BASE_BORROW_MODE");

            migrationBuilder.DropTable(
                name: "BASE_WATERMARK");

            migrationBuilder.DropTable(
                name: "CASE_SHELF_MANAGEMENT");

            migrationBuilder.DropTable(
                name: "DEPT_MEUM_TREE");

            migrationBuilder.DropTable(
                name: "ESSENTIAL_DOCUMENTS");

            migrationBuilder.DropTable(
                name: "FILES_HIS");

            migrationBuilder.DropTable(
                name: "FILES_OTHER");

            migrationBuilder.DropTable(
                name: "INPATIENT_INFO");

            migrationBuilder.DropTable(
                name: "MedicalRecor");

            migrationBuilder.DropTable(
                name: "NODE_APPROVER");

            migrationBuilder.DropTable(
                name: "OUTPATIENT_INFO");

            migrationBuilder.DropTable(
                name: "PAY_CONFIG");

            migrationBuilder.DropTable(
                name: "RECALL_APPLY_AND_ARCHIVAL");

            migrationBuilder.DropTable(
                name: "RECALL_APPROVER");

            migrationBuilder.DropTable(
                name: "SYS_OPER_LOG");

            migrationBuilder.DropTable(
                name: "ARCHIVE_APPLY");

            migrationBuilder.DropTable(
                name: "PROCESS_NODE");

            migrationBuilder.DropTable(
                name: "RECALL_APPLY");

            migrationBuilder.DropTable(
                name: "PROCESS_DESIGN");
        }
    }
}

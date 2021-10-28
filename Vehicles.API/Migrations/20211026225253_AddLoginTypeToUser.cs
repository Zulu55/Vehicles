using Microsoft.EntityFrameworkCore.Migrations;

namespace Vehicles.API.Migrations
{
    public partial class AddLoginTypeToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DocumentTypes_DocumentTypeId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentTypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "LoginType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SocialImageURL",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DocumentTypes_DocumentTypeId",
                table: "AspNetUsers",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DocumentTypes_DocumentTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LoginType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SocialImageURL",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentTypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DocumentTypes_DocumentTypeId",
                table: "AspNetUsers",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

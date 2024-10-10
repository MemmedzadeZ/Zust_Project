using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zust.Entity.Migrations
{
    /// <inheritdoc />
    public partial class Init17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CustomUserId",
                table: "Comments",
                column: "CustomUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_CustomUserId",
                table: "Comments",
                column: "CustomUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_CustomUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CustomUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CustomUserId",
                table: "Comments");
        }
    }
}

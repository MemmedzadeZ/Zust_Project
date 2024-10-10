using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zust.Entity.Migrations
{
    /// <inheritdoc />
    public partial class Init16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageOrVideoUrl",
                table: "Posts",
                newName: "VideoUrl");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "VideoUrl",
                table: "Posts",
                newName: "ImageOrVideoUrl");
        }
    }
}

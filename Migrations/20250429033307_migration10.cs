using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThirdDelivery.Migrations
{
    /// <inheritdoc />
    public partial class migration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "FriendSuggestions",
                newName: "ImagePath");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "FriendSuggestions",
                newName: "ImageUrl");
        }
    }
}

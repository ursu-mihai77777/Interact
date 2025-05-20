using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThirdDelivery.Migrations
{
    /// <inheritdoc />
    public partial class migration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MutualFriends",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "Friends",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Friends",
                newName: "FriendId");

            migrationBuilder.AddColumn<int>(
                name: "MutualFriends",
                table: "Friends",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

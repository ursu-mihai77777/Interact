using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThirdDelivery.Migrations
{
    /// <inheritdoc />
    public partial class migration29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGroupPost",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGroupPost",
                table: "Posts");
        }
    }
}

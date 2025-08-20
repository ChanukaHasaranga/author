using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorDBContext.Migrations
{
    /// <inheritdoc />
    public partial class profilepicturecolum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureURL",
                table: "Author",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureURL",
                table: "Author");
        }
    }
}

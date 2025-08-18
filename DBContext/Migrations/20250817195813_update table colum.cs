using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorDBContext.Migrations
{
    /// <inheritdoc />
    public partial class updatetablecolum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Author",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Author",
                newName: "Password");
        }
    }
}

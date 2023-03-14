using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogTutorial.Migrations
{
    /// <inheritdoc />
    public partial class updateProfileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "TblProfaie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "TblProfaie");
        }
    }
}

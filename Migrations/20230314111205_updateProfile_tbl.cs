using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogTutorial.Migrations
{
    /// <inheritdoc />
    public partial class updateProfile_tbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "TblProfaie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "TblProfaie");
        }
    }
}

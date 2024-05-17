using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KufarAnalyzer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSubway : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subway",
                table: "KufarFlats",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subway",
                table: "KufarFlats");
        }
    }
}

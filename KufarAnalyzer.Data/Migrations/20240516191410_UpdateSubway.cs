using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KufarAnalyzer.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubway : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Subway",
                table: "KufarFlats",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Subway",
                table: "KufarFlats",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

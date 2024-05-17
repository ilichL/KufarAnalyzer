using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KufarAnalyzer.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKufarFlat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomsNumber",
                table: "KufarFlats");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "KufarFlats",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "KufarFlats",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "KufarFlats",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "KufarFlats",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomsNumber",
                table: "KufarFlats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

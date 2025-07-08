using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OwnerEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerEmail",
                table: "Units",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerEmail",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerEmail",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "OwnerEmail",
                table: "Properties");
        }
    }
}

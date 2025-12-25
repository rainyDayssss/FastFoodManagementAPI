using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFoodManagementAPI.Migrations;

/// <inheritdoc />
public partial class AddedIsActiveinProduct : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Products",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AddColumn<bool>(
            name: "IsActive",
            table: "Products",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.CreateIndex(
            name: "IX_Product_Name",
            table: "Products",
            column: "Name");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Product_Name",
            table: "Products");

        migrationBuilder.DropColumn(
            name: "IsActive",
            table: "Products");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Products",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");
    }
}

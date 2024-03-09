using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI1.Migrations
{
    public partial class literowka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactNubmer",
                table: "Restaurants",
                newName: "ContactNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "Restaurants",
                newName: "ContactNubmer");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDRecipeEF.BL.DL.Migrations
{
    public partial class CreateRestaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Recipes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Ingredients",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RestauarantName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RestaurantId",
                table: "Recipes",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RestaurantId",
                table: "Ingredients",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Restaurants_RestaurantId",
                table: "Ingredients",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Restaurants_RestaurantId",
                table: "Recipes",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Restaurants_RestaurantId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Restaurants_RestaurantId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_RestaurantId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RestaurantId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Ingredients");
        }
    }
}

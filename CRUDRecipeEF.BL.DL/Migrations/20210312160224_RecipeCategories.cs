using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDRecipeEF.BL.DL.Migrations
{
    public partial class RecipeCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Category_CategoryId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.CreateTable(
                name: "RecipeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCategories", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeCategories_CategoryId",
                table: "Recipes",
                column: "CategoryId",
                principalTable: "RecipeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeCategories_CategoryId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "RecipeCategories");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Category_CategoryId",
                table: "Recipes",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

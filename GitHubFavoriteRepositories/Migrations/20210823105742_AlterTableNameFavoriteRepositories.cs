using Microsoft.EntityFrameworkCore.Migrations;

namespace GitHubFavoriteRepositories.Migrations
{
    public partial class AlterTableNameFavoriteRepositories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.RenameTable(
                name: "Favorites",
                newName: "FavoriteRepositories");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_RepositoryId",
                table: "FavoriteRepositories",
                newName: "IX_FavoriteRepositories_RepositoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRepositories",
                table: "FavoriteRepositories",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRepositories",
                table: "FavoriteRepositories");

            migrationBuilder.RenameTable(
                name: "FavoriteRepositories",
                newName: "Favorites");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRepositories_RepositoryId",
                table: "Favorites",
                newName: "IX_Favorites_RepositoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");
        }
    }
}

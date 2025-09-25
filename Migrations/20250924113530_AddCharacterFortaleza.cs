using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameOfThronesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterFortaleza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Strongholds_StrongholdId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "StrongholdId",
                table: "Characters",
                newName: "FortalezaId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_StrongholdId",
                table: "Characters",
                newName: "IX_Characters_FortalezaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Strongholds_FortalezaId",
                table: "Characters",
                column: "FortalezaId",
                principalTable: "Strongholds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Strongholds_FortalezaId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "FortalezaId",
                table: "Characters",
                newName: "StrongholdId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_FortalezaId",
                table: "Characters",
                newName: "IX_Characters_StrongholdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Strongholds_StrongholdId",
                table: "Characters",
                column: "StrongholdId",
                principalTable: "Strongholds",
                principalColumn: "Id");
        }
    }
}

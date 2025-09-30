using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameOfThronesAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatePg12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Idade = table.Column<int>(type: "integer", nullable: false),
                    Lema = table.Column<string>(type: "text", nullable: false),
                    StrongholdId = table.Column<int>(type: "integer", nullable: true),
                    OverlordHouseId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Houses_Houses_OverlordHouseId",
                        column: x => x.OverlordHouseId,
                        principalTable: "Houses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Strongholds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Localizacao = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    HouseId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strongholds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Strongholds_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    HouseId = table.Column<int>(type: "integer", nullable: true),
                    NovaCasaId = table.Column<int>(type: "integer", nullable: true),
                    Titulo = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    Sexo = table.Column<string>(type: "text", nullable: false),
                    FortalezaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_Houses_NovaCasaId",
                        column: x => x.NovaCasaId,
                        principalTable: "Houses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_Strongholds_FortalezaId",
                        column: x => x.FortalezaId,
                        principalTable: "Strongholds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StrongholdId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Strongholds_StrongholdId",
                        column: x => x.StrongholdId,
                        principalTable: "Strongholds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacterEvent",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEvent", x => new { x.CharactersId, x.EventId });
                    table.ForeignKey(
                        name: "FK_CharacterEvent_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterEvent_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventHouse",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    HousesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventHouse", x => new { x.EventId, x.HousesId });
                    table.ForeignKey(
                        name: "FK_EventHouse_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventHouse_Houses_HousesId",
                        column: x => x.HousesId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEvent_EventId",
                table: "CharacterEvent",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_FortalezaId",
                table: "Characters",
                column: "FortalezaId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_HouseId",
                table: "Characters",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_NovaCasaId",
                table: "Characters",
                column: "NovaCasaId");

            migrationBuilder.CreateIndex(
                name: "IX_EventHouse_HousesId",
                table: "EventHouse",
                column: "HousesId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_StrongholdId",
                table: "Events",
                column: "StrongholdId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_OverlordHouseId",
                table: "Houses",
                column: "OverlordHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Strongholds_HouseId",
                table: "Strongholds",
                column: "HouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEvent");

            migrationBuilder.DropTable(
                name: "EventHouse");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Strongholds");

            migrationBuilder.DropTable(
                name: "Houses");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StripePortfolio.Migrations
{
    /// <inheritdoc />
    public partial class _342 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardType_Card_CardId",
                table: "CardType");

            migrationBuilder.DropForeignKey(
                name: "FK_Element_Card_CardId",
                table: "Element");

            migrationBuilder.DropForeignKey(
                name: "FK_Set_Card_CardId",
                table: "Set");

            migrationBuilder.DropForeignKey(
                name: "FK_Subtype_Card_CardId",
                table: "Subtype");

            migrationBuilder.DropIndex(
                name: "IX_Subtype_CardId",
                table: "Subtype");

            migrationBuilder.DropIndex(
                name: "IX_Set_CardId",
                table: "Set");

            migrationBuilder.DropIndex(
                name: "IX_Element_CardId",
                table: "Element");

            migrationBuilder.DropIndex(
                name: "IX_CardType_CardId",
                table: "CardType");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Subtype");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Set");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Element");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "CardType");

            migrationBuilder.CreateTable(
                name: "CardCardSet",
                columns: table => new
                {
                    CardsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SetsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardCardSet", x => new { x.CardsId, x.SetsId });
                    table.ForeignKey(
                        name: "FK_CardCardSet_Card_CardsId",
                        column: x => x.CardsId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardCardSet_Set_SetsId",
                        column: x => x.SetsId,
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardCardType",
                columns: table => new
                {
                    CardTypesId = table.Column<int>(type: "INTEGER", nullable: false),
                    CardsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardCardType", x => new { x.CardTypesId, x.CardsId });
                    table.ForeignKey(
                        name: "FK_CardCardType_CardType_CardTypesId",
                        column: x => x.CardTypesId,
                        principalTable: "CardType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardCardType_Card_CardsId",
                        column: x => x.CardsId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardElement",
                columns: table => new
                {
                    CardsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ElementsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardElement", x => new { x.CardsId, x.ElementsId });
                    table.ForeignKey(
                        name: "FK_CardElement_Card_CardsId",
                        column: x => x.CardsId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardElement_Element_ElementsId",
                        column: x => x.ElementsId,
                        principalTable: "Element",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardSubtype",
                columns: table => new
                {
                    CardsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubtypesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSubtype", x => new { x.CardsId, x.SubtypesId });
                    table.ForeignKey(
                        name: "FK_CardSubtype_Card_CardsId",
                        column: x => x.CardsId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardSubtype_Subtype_SubtypesId",
                        column: x => x.SubtypesId,
                        principalTable: "Subtype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardCardSet_SetsId",
                table: "CardCardSet",
                column: "SetsId");

            migrationBuilder.CreateIndex(
                name: "IX_CardCardType_CardsId",
                table: "CardCardType",
                column: "CardsId");

            migrationBuilder.CreateIndex(
                name: "IX_CardElement_ElementsId",
                table: "CardElement",
                column: "ElementsId");

            migrationBuilder.CreateIndex(
                name: "IX_CardSubtype_SubtypesId",
                table: "CardSubtype",
                column: "SubtypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardCardSet");

            migrationBuilder.DropTable(
                name: "CardCardType");

            migrationBuilder.DropTable(
                name: "CardElement");

            migrationBuilder.DropTable(
                name: "CardSubtype");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Subtype",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Set",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Element",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "CardType",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subtype_CardId",
                table: "Subtype",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Set_CardId",
                table: "Set",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Element_CardId",
                table: "Element",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardType_CardId",
                table: "CardType",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardType_Card_CardId",
                table: "CardType",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Element_Card_CardId",
                table: "Element",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Set_Card_CardId",
                table: "Set",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtype_Card_CardId",
                table: "Subtype",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id");
        }
    }
}

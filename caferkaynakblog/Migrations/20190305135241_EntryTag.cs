using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace caferkaynakblog.Migrations
{
    public partial class EntryTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Tags_TagId",
                table: "Entries");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "Entries",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "EntryTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TagId = table.Column<int>(nullable: false),
                    EntryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryTags_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntryTags_EntryId",
                table: "EntryTags",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryTags_TagId",
                table: "EntryTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Tags_TagId",
                table: "Entries",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Tags_TagId",
                table: "Entries");

            migrationBuilder.DropTable(
                name: "EntryTags");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "Entries",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Tags_TagId",
                table: "Entries",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

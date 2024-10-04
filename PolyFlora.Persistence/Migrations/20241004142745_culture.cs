using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolyFlora.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class culture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Flowers");

            migrationBuilder.CreateTable(
                name: "FlowerCulture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageCode = table.Column<int>(type: "integer", nullable: false),
                    TargCulture = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FlowerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerCulture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowerCulture_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowerCulture_FlowerId",
                table: "FlowerCulture",
                column: "FlowerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowerCulture");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Flowers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Flowers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolyFlora.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BouquetFlower_Bouquet_BouquetsId",
                table: "BouquetFlower");

            migrationBuilder.DropForeignKey(
                name: "FK_BouquetSize_Bouquet_BouquetId",
                table: "BouquetSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bouquet",
                table: "Bouquet");

            migrationBuilder.RenameTable(
                name: "Bouquet",
                newName: "Bouquets");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Flowers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "BouquetSize",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Bouquets",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bouquets",
                table: "Bouquets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ImageId",
                table: "Users",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Flowers_ImageId",
                table: "Flowers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_BouquetSize_ImageId",
                table: "BouquetSize",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Bouquets_ImageId",
                table: "Bouquets",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_BouquetFlower_Bouquets_BouquetsId",
                table: "BouquetFlower",
                column: "BouquetsId",
                principalTable: "Bouquets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bouquets_Images_ImageId",
                table: "Bouquets",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BouquetSize_Bouquets_BouquetId",
                table: "BouquetSize",
                column: "BouquetId",
                principalTable: "Bouquets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BouquetSize_Images_ImageId",
                table: "BouquetSize",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flowers_Images_ImageId",
                table: "Flowers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ImageId",
                table: "Users",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BouquetFlower_Bouquets_BouquetsId",
                table: "BouquetFlower");

            migrationBuilder.DropForeignKey(
                name: "FK_Bouquets_Images_ImageId",
                table: "Bouquets");

            migrationBuilder.DropForeignKey(
                name: "FK_BouquetSize_Bouquets_BouquetId",
                table: "BouquetSize");

            migrationBuilder.DropForeignKey(
                name: "FK_BouquetSize_Images_ImageId",
                table: "BouquetSize");

            migrationBuilder.DropForeignKey(
                name: "FK_Flowers_Images_ImageId",
                table: "Flowers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ImageId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Users_ImageId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Flowers_ImageId",
                table: "Flowers");

            migrationBuilder.DropIndex(
                name: "IX_BouquetSize_ImageId",
                table: "BouquetSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bouquets",
                table: "Bouquets");

            migrationBuilder.DropIndex(
                name: "IX_Bouquets_ImageId",
                table: "Bouquets");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "BouquetSize");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Bouquets");

            migrationBuilder.RenameTable(
                name: "Bouquets",
                newName: "Bouquet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bouquet",
                table: "Bouquet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BouquetFlower_Bouquet_BouquetsId",
                table: "BouquetFlower",
                column: "BouquetsId",
                principalTable: "Bouquet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BouquetSize_Bouquet_BouquetId",
                table: "BouquetSize",
                column: "BouquetId",
                principalTable: "Bouquet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

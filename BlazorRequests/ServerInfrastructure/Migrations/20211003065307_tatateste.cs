using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Infrastructure.Migrations
{
    public partial class tatateste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "TB_CAR",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TB_PHOTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PHOTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_PHOTO_TB_CAR_CarId",
                        column: x => x.CarId,
                        principalTable: "TB_CAR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PHOTO_CarId",
                table: "TB_PHOTO",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PHOTO");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "TB_CAR");
        }
    }
}

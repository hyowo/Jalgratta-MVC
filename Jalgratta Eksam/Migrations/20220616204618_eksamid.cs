using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jalgratta_Eksam.Migrations
{
    public partial class eksamid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eksamid",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eesnimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Perekonnanimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Teooria = table.Column<int>(type: "int", nullable: false),
                    Slaalom = table.Column<int>(type: "int", nullable: false),
                    Ring = table.Column<int>(type: "int", nullable: false),
                    Tanav = table.Column<int>(type: "int", nullable: false),
                    Luba = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eksamid", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eksamid");
        }
    }
}

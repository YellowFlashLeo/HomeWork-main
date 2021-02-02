using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

namespace Number.Db.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstIndex = table.Column<int>(type: "int", nullable: false),
                    LastIndex = table.Column<int>(type: "int", nullable: false),
                    FebonacciSequence = table.Column<List<int>>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inputs", x => x.Id);
                }) ;
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inputs");
        }
    }
}

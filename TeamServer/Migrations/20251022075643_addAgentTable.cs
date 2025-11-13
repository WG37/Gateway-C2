using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamServer.Migrations
{
    /// <inheritdoc />
    public partial class addAgentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    metadata_Hostname = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    metadata_Username = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    metadata_ProcessName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    metadata_ProcessId = table.Column<int>(type: "int", nullable: false),
                    metadata_Architecture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    metadata_Integrity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_UniqueId",
                table: "Agents",
                column: "UniqueId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agents");
        }
    }
}

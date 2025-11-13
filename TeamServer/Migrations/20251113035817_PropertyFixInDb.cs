using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamServer.Migrations
{
    /// <inheritdoc />
    public partial class PropertyFixInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "metadata_Username",
                table: "Agents",
                newName: "Metadata_Username");

            migrationBuilder.RenameColumn(
                name: "metadata_ProcessName",
                table: "Agents",
                newName: "Metadata_ProcessName");

            migrationBuilder.RenameColumn(
                name: "metadata_ProcessId",
                table: "Agents",
                newName: "Metadata_ProcessId");

            migrationBuilder.RenameColumn(
                name: "metadata_Integrity",
                table: "Agents",
                newName: "Metadata_Integrity");

            migrationBuilder.RenameColumn(
                name: "metadata_Hostname",
                table: "Agents",
                newName: "Metadata_Hostname");

            migrationBuilder.RenameColumn(
                name: "metadata_Architecture",
                table: "Agents",
                newName: "Metadata_Architecture");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Metadata_Username",
                table: "Agents",
                newName: "metadata_Username");

            migrationBuilder.RenameColumn(
                name: "Metadata_ProcessName",
                table: "Agents",
                newName: "metadata_ProcessName");

            migrationBuilder.RenameColumn(
                name: "Metadata_ProcessId",
                table: "Agents",
                newName: "metadata_ProcessId");

            migrationBuilder.RenameColumn(
                name: "Metadata_Integrity",
                table: "Agents",
                newName: "metadata_Integrity");

            migrationBuilder.RenameColumn(
                name: "Metadata_Hostname",
                table: "Agents",
                newName: "metadata_Hostname");

            migrationBuilder.RenameColumn(
                name: "Metadata_Architecture",
                table: "Agents",
                newName: "metadata_Architecture");
        }
    }
}

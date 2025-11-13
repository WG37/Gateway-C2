using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamServer.Migrations
{
    /// <inheritdoc />
    public partial class addedSetter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_httpListeners",
                table: "httpListeners");

            migrationBuilder.RenameTable(
                name: "httpListeners",
                newName: "HttpListeners");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "HttpListeners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HttpListeners",
                table: "HttpListeners",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HttpListener_BindPort",
                table: "HttpListeners",
                sql: "[BindPort] BETWEEN 1024 AND 65535");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HttpListeners",
                table: "HttpListeners");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HttpListener_BindPort",
                table: "HttpListeners");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "HttpListeners");

            migrationBuilder.RenameTable(
                name: "HttpListeners",
                newName: "httpListeners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_httpListeners",
                table: "httpListeners",
                column: "Id");
        }
    }
}

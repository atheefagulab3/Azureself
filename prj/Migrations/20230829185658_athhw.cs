using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prj.Migrations
{
    /// <inheritdoc />
    public partial class athhw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiles", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "additional_Travellers",
                columns: table => new
                {
                    AdditionalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    AdditionalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileCustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additional_Travellers", x => x.AdditionalId);
                    table.ForeignKey(
                        name: "FK_additional_Travellers_profiles_ProfileCustomerId",
                        column: x => x.ProfileCustomerId,
                        principalTable: "profiles",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_additional_Travellers_ProfileCustomerId",
                table: "additional_Travellers",
                column: "ProfileCustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "additional_Travellers");

            migrationBuilder.DropTable(
                name: "profiles");
        }
    }
}

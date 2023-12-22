using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDonationAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "CreatedDate",
            //    table: "AspNetUsers",
            //    type: "datetime2",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "FirstName",
            //    table: "AspNetUsers",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNGO",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "LastName",
            //    table: "AspNetUsers",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "ModifiedBy",
            //    table: "AspNetUsers",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "ModifiedDate",
            //    table: "AspNetUsers",
            //    type: "datetime2",
            //    nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "Status",
            //    table: "AspNetUsers",
            //    type: "int",
            //    nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "CreatedDate",
            //    table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "FirstName",
            //    table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsNGO",
                table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "LastName",
            //    table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "ModifiedBy",
            //    table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "ModifiedDate",
            //    table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "Status",
            //    table: "AspNetUsers");
        }
    }
}

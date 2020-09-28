using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class ChangedStoreOwnerToAuditedAggregateRoot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "EasyAbpEShopStoresStoreOwners");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "EasyAbpEShopStoresStoreOwners");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EasyAbpEShopStoresStoreOwners");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "EasyAbpEShopStoresStoreOwners",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "EasyAbpEShopStoresStoreOwners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EasyAbpEShopStoresStoreOwners",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

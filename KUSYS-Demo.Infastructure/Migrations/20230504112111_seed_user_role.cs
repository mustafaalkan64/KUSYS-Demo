using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUSYS_Demo.Infastructure.Migrations
{
    public partial class seed_user_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53cda9e5-57d6-40e8-bf15-ae1ee769276d", "53cda9e5-57d6-40e8-bf15-ae1ee769276d", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "878c3a65-d0c8-49f6-b34b-2696a402d324", "AQAAAAEAACcQAAAAELN1bSrutfWbnjkYZxsBL2NvXGp6mEwDpvZIiprySq/ripvZ4MAV+TUwpskJLtSOsQ==", "6f495cb5-57e4-4093-9a20-658c5e4ee535" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53cda9e5-57d6-40e8-bf15-ae1ee769276d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30659213-f12d-43eb-aba5-ad00b41e0b34", "AQAAAAEAACcQAAAAEFUeLIxChq1iOvSfTxHIpn9XsBeo6gmU4K1kLpQiUZcVkOmnMcge1s3JPgjvQDWeeQ==", "aa9e1d90-f627-4383-806f-ccfe8f0c89e8" });
        }
    }
}

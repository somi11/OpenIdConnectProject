using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cmos.IDP.Migrations
{
    /// <inheritdoc />
    public partial class emailadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("094ad856-82ca-4d46-9c15-85835c83c140"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("11b657c5-8471-40e3-87e3-a36485eeefa3"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("588dece9-41eb-4f88-804c-43af65f97aac"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("77a0d7aa-549e-4753-86c5-cd18cc994e57"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("7ec76dc4-20b8-485a-9914-b9fa186877fd"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("85c3c12f-065c-42f5-bb9f-7f28dcffe3f1"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("d0478761-999c-4320-9f61-b7a4397e2e18"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("fdd91313-79e8-4e19-865f-0313c90bd894"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityCode",
                table: "Users",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecurityCodeExpirationDate",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("0b884b2f-f7c2-4e0d-a342-d031433e6e41"), "d131963e-f0c6-47dc-873a-1b4eb01bfd3f", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" },
                    { new Guid("1516c4e6-b333-4a11-bbe4-e29f8c861761"), "bac79581-2401-4292-9912-4b273a346a12", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" },
                    { new Guid("52627763-5804-43b5-b92b-ee51574405a6"), "2bd0844e-e758-47a3-bb76-faba88913728", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" },
                    { new Guid("613bcab0-65bd-40d8-ae2a-84f9d3a665ff"), "95cc5b84-7803-4ed2-b61e-48662690a9fa", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" },
                    { new Guid("6cbdd78e-847d-40d1-aad0-e8c6f610abf9"), "c9c27bbe-c5a3-4812-a6bc-5949fa2721c3", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" },
                    { new Guid("8d9fe1c8-96ab-4cc4-a44e-103340ae0518"), "085e7fcc-f07d-415b-bbe1-5589a22b460b", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" },
                    { new Guid("9ab0c5f8-d8ce-480a-abbe-8a831a8fca6e"), "9fc76840-0f4b-4e4c-8f41-79cef13af09b", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" },
                    { new Guid("a8b9641c-a42c-4aea-a256-7ccd1772695d"), "77de8f21-578e-45b7-9b98-d4caca8e4749", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                columns: new[] { "ConcurrencyStamp", "Email", "SecurityCode", "SecurityCodeExpirationDate" },
                values: new object[] { "263948b3-0406-41c5-af5f-d68507faedf8", "david@cmos.com", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                columns: new[] { "ConcurrencyStamp", "Email", "SecurityCode", "SecurityCodeExpirationDate" },
                values: new object[] { "b07bb344-5b68-4393-a920-b7b28f963de5", "emma@cmos.com", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("0b884b2f-f7c2-4e0d-a342-d031433e6e41"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("1516c4e6-b333-4a11-bbe4-e29f8c861761"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("52627763-5804-43b5-b92b-ee51574405a6"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("613bcab0-65bd-40d8-ae2a-84f9d3a665ff"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("6cbdd78e-847d-40d1-aad0-e8c6f610abf9"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("8d9fe1c8-96ab-4cc4-a44e-103340ae0518"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("9ab0c5f8-d8ce-480a-abbe-8a831a8fca6e"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("a8b9641c-a42c-4aea-a256-7ccd1772695d"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityCodeExpirationDate",
                table: "Users");

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("094ad856-82ca-4d46-9c15-85835c83c140"), "bc14b497-dc4a-443e-b831-cc9f7e59d9f8", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" },
                    { new Guid("11b657c5-8471-40e3-87e3-a36485eeefa3"), "01da1f97-5939-4e2d-b99e-659aff4c0114", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" },
                    { new Guid("588dece9-41eb-4f88-804c-43af65f97aac"), "563f536f-fa84-4308-ad13-525ba0a014c7", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" },
                    { new Guid("77a0d7aa-549e-4753-86c5-cd18cc994e57"), "8eae4fbf-23d1-45cc-981c-0bd35cc56537", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" },
                    { new Guid("7ec76dc4-20b8-485a-9914-b9fa186877fd"), "9a7b5a5c-c751-4ce2-896a-4b717cbc1e07", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" },
                    { new Guid("85c3c12f-065c-42f5-bb9f-7f28dcffe3f1"), "88c980a1-fb84-4767-9e12-1ecd83e7aa13", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" },
                    { new Guid("d0478761-999c-4320-9f61-b7a4397e2e18"), "b8a08321-90e6-4fd5-bf7e-902a77661958", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" },
                    { new Guid("fdd91313-79e8-4e19-865f-0313c90bd894"), "e4430dd7-1654-4762-80dc-2a0ce3bedb9e", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                column: "ConcurrencyStamp",
                value: "1efdb57d-0ca5-4fcd-a2e2-0dc4b29b1722");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                column: "ConcurrencyStamp",
                value: "dd2cde2f-0ba9-43ef-aa5a-be144e28fb8b");
        }
    }
}

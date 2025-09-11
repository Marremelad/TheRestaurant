using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class Init_20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 8, 41, 47, 174, DateTimeKind.Utc).AddTicks(7803));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 8, 41, 47, 174, DateTimeKind.Utc).AddTicks(7804));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 8, 41, 47, 174, DateTimeKind.Utc).AddTicks(7805));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName" },
                values: new object[] { new DateTime(2025, 9, 11, 8, 41, 47, 174, DateTimeKind.Utc).AddTicks(7806), "Erika.James@email.com", "Erika", "James" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 8, 41, 47, 174, DateTimeKind.Utc).AddTicks(7807));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 8, 41, 47, 174, DateTimeKind.Utc).AddTicks(7808));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 11, 8, 41, 47, 174, DateTimeKind.Utc).AddTicks(6814), "$2a$11$p6p6fLISr01ohsDhK8v6JuXV93Lk.z1v4ygRRo5oBQO0GBxUaFm0a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5994));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5995));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5996));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Email", "FirstName", "LastName" },
                values: new object[] { new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5997), "emily.davis@email.com", "Emily", "Davis" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5998));

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5999));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 11, 8, 33, 4, 656, DateTimeKind.Utc).AddTicks(5079), "$2a$11$omBoaf8eFt46vEoWgP9Eh.35usz9tcdkIF/eHmWJrZr8ojKqP3nQO" });
        }
    }
}

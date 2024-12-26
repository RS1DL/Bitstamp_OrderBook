using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderBook.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderBooks",
                columns: table => new
                {
                    OrderBookId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeStamp = table.Column<string>(type: "text", nullable: false),
                    MicroTimeStamp = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBooks", x => x.OrderBookId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderBookId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderBookEntityOrderBookId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrderBookEntityOrderBookId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_OrderBooks_OrderBookEntityOrderBookId",
                        column: x => x.OrderBookEntityOrderBookId,
                        principalTable: "OrderBooks",
                        principalColumn: "OrderBookId");
                    table.ForeignKey(
                        name: "FK_Orders_OrderBooks_OrderBookEntityOrderBookId1",
                        column: x => x.OrderBookEntityOrderBookId1,
                        principalTable: "OrderBooks",
                        principalColumn: "OrderBookId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderBookEntityOrderBookId",
                table: "Orders",
                column: "OrderBookEntityOrderBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderBookEntityOrderBookId1",
                table: "Orders",
                column: "OrderBookEntityOrderBookId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderBooks");
        }
    }
}

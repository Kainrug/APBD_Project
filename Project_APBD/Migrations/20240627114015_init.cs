using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_APBD.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Software",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSubscriptionAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsOneTimePurchaseAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCustomer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Krs = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCustomer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyCustomer_Customer_Id",
                        column: x => x.Id,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualCustomer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualCustomer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualCustomer_Customer_Id",
                        column: x => x.Id,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupportYears = table.Column<int>(type: "int", nullable: false),
                    discount_id = table.Column<int>(type: "int", nullable: true),
                    software_version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_signed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Discount_discount_id",
                        column: x => x.discount_id,
                        principalTable: "Discount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contract_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "Email", "IsDeleted", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main St", "john.doe@example.com", false, "123456789" },
                    { 2, "456 Elm St", "jane.smith@example.com", false, "987654321" },
                    { 3, "789 Oak St", "info@company.com", false, "555123456" },
                    { 4, "321 Pine St", "contact@business.com", false, "555987654" }
                });

            migrationBuilder.InsertData(
                table: "Discount",
                columns: new[] { "Id", "DiscountType", "EndDate", "Name", "StartDate", "Value" },
                values: new object[] { 1, 1, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black Friday Discount", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.0 });

            migrationBuilder.InsertData(
                table: "Software",
                columns: new[] { "Id", "Category", "CurrentVersion", "Description", "IsOneTimePurchaseAvailable", "IsSubscriptionAvailable", "Name" },
                values: new object[,]
                {
                    { 1, "Finance", "1.0", "Financial software", true, false, "FinanceApp" },
                    { 2, "Education", "2.1", "Educational software", true, false, "EduApp" }
                });

            migrationBuilder.InsertData(
                table: "CompanyCustomer",
                columns: new[] { "Id", "CompanyName", "Krs" },
                values: new object[,]
                {
                    { 3, "Company Inc.", "1234567890" },
                    { 4, "Business LLC", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "IndividualCustomer",
                columns: new[] { "Id", "FirstName", "LastName", "Pesel" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "12345678901" },
                    { 2, "Jane", "Smith", "98765432109" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CustomerId",
                table: "Contract",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_discount_id",
                table: "Contract",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_SoftwareId",
                table: "Contract",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ContractId",
                table: "Payment",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyCustomer");

            migrationBuilder.DropTable(
                name: "IndividualCustomer");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "Software");
        }
    }
}

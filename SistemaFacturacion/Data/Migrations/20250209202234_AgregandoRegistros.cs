using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaFacturacion.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoRegistros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuentasXcobrar",
                columns: table => new
                {
                    CuentasXcobrarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasXcobrar", x => x.CuentasXcobrarId);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    VentasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.VentasId);
                });

            migrationBuilder.CreateTable(
                name: "Financiamientos",
                columns: table => new
                {
                    FinanciamientoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormaPago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ganancia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financiamientos", x => x.FinanciamientoId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PorcentajeGanancia = table.Column<int>(type: "int", nullable: false),
                    PrecioVenta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "FacturaDetalle",
                columns: table => new
                {
                    VentaDetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Impuesto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CuentasXcobrarId = table.Column<int>(type: "int", nullable: true),
                    FinanciamientoId = table.Column<int>(type: "int", nullable: true),
                    VentasId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaDetalle", x => x.VentaDetalleId);
                    table.ForeignKey(
                        name: "FK_FacturaDetalle_CuentasXcobrar_CuentasXcobrarId",
                        column: x => x.CuentasXcobrarId,
                        principalTable: "CuentasXcobrar",
                        principalColumn: "CuentasXcobrarId");
                    table.ForeignKey(
                        name: "FK_FacturaDetalle_Facturas_VentasId",
                        column: x => x.VentasId,
                        principalTable: "Facturas",
                        principalColumn: "VentasId");
                    table.ForeignKey(
                        name: "FK_FacturaDetalle_Financiamientos_FinanciamientoId",
                        column: x => x.FinanciamientoId,
                        principalTable: "Financiamientos",
                        principalColumn: "FinanciamientoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturaDetalle_CuentasXcobrarId",
                table: "FacturaDetalle",
                column: "CuentasXcobrarId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaDetalle_FinanciamientoId",
                table: "FacturaDetalle",
                column: "FinanciamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaDetalle_VentasId",
                table: "FacturaDetalle",
                column: "VentasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturaDetalle");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "CuentasXcobrar");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Financiamientos");
        }
    }
}

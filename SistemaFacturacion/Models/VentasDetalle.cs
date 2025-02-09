using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFacturacion.Models;

public class VentasDetalle
{
    [Key]
    public int VentaDetalleId { get; set; }


    [ForeignKey("Productos")]
    public int ProductoId { get; set; }



    [Range(1, 10000, ErrorMessage = "Debe elegir una cantidad entre 1 y 10,000")]
    public int Cantidad { get; set; }

    public decimal PrecioVenta { get; set; }

    public decimal Descuento { get; set; }

    public decimal Impuesto { get; set; }
    public decimal SubTotal { get; set; }


}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFacturacion.Models;

public class Productos
{
	[Key]
	public int ProductoId { get; set; }

    [Required(ErrorMessage = "Debe ingresar un nombre para el producto")]
    public string Nombre { get; set; }

	[Required(ErrorMessage = "Debe ingresar una descripción para el producto")]
	public string Descripcion { get; set; }

	[Range(0.01, 500000, ErrorMessage = "Debe elegir un costo entre $0.01 y $500,000")]
	public int Costo { get; set; }

    [Range(1, 10000, ErrorMessage = "Debe elegir una cantidad entre 1 y 10,000")]
    public int Cantidad { get; set; }

    public int PorcentajeGanancia { get; set; }

    [Range(1, 1000000000000000, ErrorMessage = "Debe elegir una cantidad ")]
    public int PrecioVenta { get; set; }


}

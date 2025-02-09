using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFacturacion.Models
{

    public class Financiamiento
    {
        [Key]
        public int FinanciamientoId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Debe ingresar un nombre para el cliente")]
        public string NombreCliente { get; set; }

        [Required(ErrorMessage = "Debe ingresar una dirección para el cliente")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe ingresar un teléfono para el cliente")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Debe ingresar un email para el cliente")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar una forma de pago")]
        public string FormaPago { get; set; }

        [Required(ErrorMessage = "Debe ingresar una forma de pago")]
        public decimal CantidadPago { get; set; }

        [Required(ErrorMessage = "Debe ingresar una forma de pago")]
        public decimal TotalPorPago { get; set; }

        [Required(ErrorMessage = "Debe ingresar una forma de pago")]
        public decimal ganancia  { get; set; }

        public ICollection<VentasDetalle> VentasDetalles { get; set; } = new List<VentasDetalle>();
    }
}

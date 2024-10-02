namespace Practica02.Models
{
    public class Factura
    {
        public int Nro { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string FormaPago { get; set; }
        public DateTime? FechaBaja { get; set; } 
        public decimal Total { get; set; }
    }
}

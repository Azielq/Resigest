namespace Servicio_Update.Models
{
    public class Class_MovimientosCredito
    {
        public int movimiento_ID { get; set; }

        public int credito_ID { get; set; }

        public string tipo_movimiento { get; set; }

        public decimal monto { get; set; }

        public DateTime? fecha_movimiento { get; set; }

        public string? descripcion { get; set; }

       
    }
}

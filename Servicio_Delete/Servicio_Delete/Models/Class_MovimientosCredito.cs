namespace Servicio_Delete.Models
{
    public class Class_MovimientosCredito
    {
        public int movimiento_ID { get; set; }

        public int credito_ID { get; set; }

        public string tipo_movimiento { get; set; }

        public decimal monto { get; set; }  // Cambiado a decimal

        public DateTime? fecha_movimiento { get; set; }
        public string? descripcion { get; set; }
    }
}

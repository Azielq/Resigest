namespace Servicio_Create.Models
{
    public class Class_Ocupaciones
    {
        public int ocupacion_ID { get; set; }

        public int habitacion_ID { get; set; }

        public string Cedula_P { get; set; } = null!;

        public DateTime? fecha_entrada { get; set; }

        public DateTime? fecha_salida { get; set; }

        public bool? uso_credito { get; set; }

        public int? credito_ID { get; set; }

        public string? estado { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Servicio_Read.Models;

public partial class Credito
{
    public int credito_ID { get; set; }

    public string Cedula_P { get; set; }

    public decimal monto_maximo { get; set; }

    public decimal saldo_actual { get; set; }
    public DateTime? fecha_creacion { get; set; }

    public DateTime? fecha_vencimiento { get; set; }
    public string? estado { get; set; }
}

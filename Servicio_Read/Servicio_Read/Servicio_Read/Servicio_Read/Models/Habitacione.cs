using System;
using System.Collections.Generic;

namespace Servicio_Read.Models;

public partial class Habitacione
{
    //public int Habitacion_ID { get; set; }

    public int habitacion_ID { get; set; }

    public string numero { get; set; } = null!;

    public int? tipo_ID { get; set; }

    public string? estado { get; set; }

  
}

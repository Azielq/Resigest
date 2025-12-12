using System;
using System.Collections.Generic;

namespace Servicio_Read.Models;

public partial class TiposHabitacion
{
    public int tipo_ID { get; set; }

    public string nombre { get; set; } = null!;

    public double precio_por_noche { get; set; }

    public string? Descripcion { get; set; }

    
}

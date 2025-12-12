using System;
using System.Collections.Generic;

namespace Servicio_Read.Models;

public partial class Role
{
    public int rol_ID { get; set; }

    public string nombre_rol { get; set; } = null!;

    public string? descripcion { get; set; }

    
}

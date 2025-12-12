using System;
using System.Collections.Generic;

namespace Servicio_Read.Models;

public partial class Persona
{
    public string Cedula_P { get; set; }

    public string Nombre_P { get; set; }

    public string? Apellido_P { get; set; }

    public string Telefono_P { get; set; }
    public string Correo_P { get; set; }

    public DateTime Fecha_Registro_P { get; set; }
    public DateTime? Fecha_Nacimiento_P { get; set; }

    public string Contrasenna_P { get; set; }

    public int Rol_ID { get; set; }


}

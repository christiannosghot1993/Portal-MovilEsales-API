using System;
using System.Collections.Generic;

namespace Portal_MovilEsales_API.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string CodigoSap { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string DireccionContacto { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public int PlantaId { get; set; }

    public string Estado { get; set; } = null!;

    public int AsesorId { get; set; }

    public virtual Asesore Asesor { get; set; } = null!;

    public virtual Planta Planta { get; set; } = null!;
}

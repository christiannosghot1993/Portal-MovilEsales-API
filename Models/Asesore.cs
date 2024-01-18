using System;
using System.Collections.Generic;

namespace Portal_MovilEsales_API.Models;

public partial class Asesore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public int PlantaId { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual Planta Planta { get; set; } = null!;
}

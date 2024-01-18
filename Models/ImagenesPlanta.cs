using System;
using System.Collections.Generic;

namespace Portal_MovilEsales_API.Models;

public partial class ImagenesPlanta
{
    public int Id { get; set; }

    public int Orden { get; set; }

    public string LinkImagen { get; set; } = null!;

    public int PlantaId { get; set; }

    public virtual Planta Planta { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Portal_MovilEsales_API.Models;

public partial class Planta
{
    public int Id { get; set; }

    public string Pais { get; set; } = null!;

    public string CodigoPlanta { get; set; } = null!;

    public string Compania { get; set; } = null!;

    public string NombrePlanta { get; set; } = null!;

    public string SalesDocumentType { get; set; } = null!;

    public string SalesOrganization { get; set; } = null!;

    public string DistributionChannel { get; set; } = null!;

    public string Division { get; set; } = null!;

    public string SalesGroup { get; set; } = null!;

    public string SalesOffice { get; set; } = null!;

    public string Kkber { get; set; } = null!;

    public int DiasPedido { get; set; }

    public string SeleccionaFecha { get; set; } = null!;

    public string PlantaXproductos { get; set; } = null!;

    public virtual ICollection<Asesore> Asesores { get; set; } = new List<Asesore>();

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<ImagenesPlanta> ImagenesPlanta { get; set; } = new List<ImagenesPlanta>();
}

using System;
using System.Collections.Generic;

namespace TallerApi.Models;

public partial class Producto
{
    public Producto(int id, string? descripcion, decimal? precio, int? stock, int? stockMinimo, int? idMarca, int? idUnidad)
    {
        Id = id;
        Descripcion = descripcion;
        Precio = precio;
        Stock = stock;
        StockMinimo = stockMinimo;
        IdMarca = idMarca;
        IdUnidad = idUnidad;
    }

    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? Stock { get; set; }

    public int? StockMinimo { get; set; }

    public int? IdMarca { get; set; }

    public int? IdUnidad { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual Unidade? IdUnidadNavigation { get; set; }

    public Producto()
    {
       
    }
}

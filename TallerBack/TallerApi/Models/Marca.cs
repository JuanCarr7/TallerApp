using System;
using System.Collections.Generic;

namespace TallerApi.Models;

public partial class Marca
{

    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    public Marca(int id, string? nombre)
    {
        Id = id;
        Nombre = nombre;
    }
    public Marca()
    {
        
    }
}

using System;
using System.Collections.Generic;

namespace TallerApi.Models;

public partial class Unidade
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public Unidade()
    {
        
    }
    public Unidade(int id, string? descripcion)
    {
        Id = id;
        Descripcion = descripcion;
       
    }
}

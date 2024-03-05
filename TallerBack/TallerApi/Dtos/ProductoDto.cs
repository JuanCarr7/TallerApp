using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerApi.Dtos
{
    public class ProductoDto
    {


        public string Nombre { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        public int? StockMinimo { get; set; }

        public string? Marca{ get; set; }
        public string? Unidad { get; set; }

        public ProductoDto(string nombre, decimal? precio, int? stock, int? stockMinimo, string? marca, string? unidad)
        {
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            StockMinimo = stockMinimo;
            Marca = marca;
            Unidad = unidad;
        }
        public ProductoDto()
        {
            Nombre = "";
            Marca = "";
            Unidad = "";
        }

        public ProductoDto(string nombre, decimal? precio, int? stock, int? stockMinimo)
        {
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            StockMinimo = stockMinimo;
        }
    }
}
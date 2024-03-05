using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerApi.Dtos;
using TallerApi.Models;

namespace TallerApi.Services
{
    public interface IProductoService
    {
        public List<ProductoDto> GetProductos();
        public ProductoDto GetProducto(string nombre);
        public ProductoDto SaveProducto(ProductoDto producto);
        public ProductoDto UpdateProducto(int id, ProductoDto producto);
        public bool DeleteProducto(int id);
        
    }
}
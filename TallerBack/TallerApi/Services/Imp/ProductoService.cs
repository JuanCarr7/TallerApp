using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TallerApi.Data;
using TallerApi.Dtos;
using TallerApi.Models;

namespace TallerApi.Services.Imp
{
    public class ProductoService : IProductoService
    {

        private TallerMecanicoPruebaContext db;

        public ProductoService(TallerMecanicoPruebaContext DataBase)
        {
            db = DataBase;
        }

        public bool DeleteProducto(int id)
        {
            try
            {
                Producto prod = db.Productos.Find(id);
                if (prod != null)
                {
                    db.Remove(prod);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (System.Exception)
            {

                return false;
            }
        }


        public ProductoDto GetProducto(string nombre)
        {
            ProductoDto prod = new();
            var producto = from p in db.Productos
                           join m in db.Marcas on p.IdMarca equals m.Id
                           join u in db.Unidades on p.IdUnidad equals u.Id
                           select new
                           {
                               Descripcion = p.Descripcion,
                               Precio = p.Precio,
                               Stock = p.Stock,
                               StockMinimo = p.StockMinimo,
                               Marca = m.Nombre,
                               Unidad = u.Descripcion
                           };
                           
            producto.ToList().ForEach(p => prod = new(p.Descripcion, p.Precio, p.Stock, p.StockMinimo,p.Marca,p.Unidad));
            return prod;
        }

        public List<ProductoDto> GetProductos()
        {
            List<ProductoDto> prod = new();
            List<Producto> productos = db.Productos.ToList();
            productos.ForEach(p => prod.Add(new ProductoDto(p.Descripcion, p.Precio, p.Stock, p.StockMinimo)));
            return prod;
        }

        public ProductoDto SaveProducto(ProductoDto producto)
        {
            try
            {
            Producto prod = new();
            prod.Descripcion = producto.Nombre;
            prod.Precio = producto.Precio;
            prod.Stock = producto.Stock;
            prod.StockMinimo = producto.StockMinimo;

            if (producto.Marca != null || producto.Marca != "")
            {
                var marca = db.Marcas.Where(m => m.Nombre == producto.Marca).FirstOrDefault();
                if (!marca.Equals(null))
                {
                    prod.IdMarca = marca.Id;
                }
            }

            if (producto.Unidad != null || producto.Unidad != "")
            {
                var unidad = db.Unidades.Where(u => u.Descripcion == producto.Unidad).FirstOrDefault();
                if (!unidad.Equals(null))
                {
                    prod.IdUnidad = unidad.Id;
                }
            }

                db.Add(prod);
                db.SaveChanges();
                return new ProductoDto(prod.Descripcion, prod.Precio, prod.Stock, prod.StockMinimo, producto.Marca, producto.Unidad);
            }
            catch (System.Exception e)
            {
                throw new Exception("Ha ocurrido un problema interno, intente nuevamente luego", e);
            }

        }

        public ProductoDto UpdateProducto(int id, ProductoDto producto)
        {
            try
            {
                Producto prod = db.Productos.Where(p => p.Id == id).First();
                if (prod == null)
                {
                    return null;
                }
                prod.Descripcion = producto.Nombre;
                prod.Precio = producto.Precio;
                prod.StockMinimo = producto.StockMinimo;
                prod.Stock = producto.Stock;
                if (producto.Marca != null || producto.Marca != "")
                {
                    var marca = db.Marcas.Where(m => m.Nombre == producto.Marca).FirstOrDefault();
                    if (!marca.Equals(null))
                    {
                        prod.IdMarca = marca.Id;
                    }
                }

                if (producto.Unidad != null || producto.Unidad != "")
                {
                    var unidad = db.Unidades.Where(u => u.Descripcion == producto.Unidad).FirstOrDefault();
                    if (!unidad.Equals(null))
                    {
                        prod.IdUnidad = unidad.Id;
                    }
                }
                db.Update(prod);
                db.SaveChanges();
                return new ProductoDto(prod.Descripcion, prod.Precio, prod.Stock, prod.StockMinimo, producto.Marca, producto.Unidad);

            }
            catch (System.Exception)
            {

                return null;
            }

        }
    }
}
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerApi.Data;
using TallerApi.Dtos;
using TallerApi.Models;
using TallerApi.Services.Imp;

namespace TestTaller.Services
{
    public class ProductoServiceTest
    {
        private async Task<TallerMecanicoPruebaContext> GetDb()
        {
            
            var options = new DbContextOptionsBuilder<TallerMecanicoPruebaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new TallerMecanicoPruebaContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Productos.CountAsync() <= 0 )
            {
                databaseContext.Unidades.Add(new Unidade(1, "Unidad"));
                databaseContext.Unidades.Add(new Unidade(2, "Litros"));
                databaseContext.Marcas.Add(new Marca(1, "Bosch"));
                databaseContext.Marcas.Add(new Marca(2, "NGK"));
                databaseContext.Marcas.Add(new Marca(3, "Castol"));
                databaseContext.Productos.Add(new Producto(1,"Filtro aceite", 10000, 10, 2,1,1));
                databaseContext.Productos.Add(new Producto(2, "Filtro aire", 20000, 20, 5, 2, 1));
                databaseContext.Productos.Add(new Producto(3, "Aceite automotor", 1000, 10, 1, 3, 2));
                databaseContext.Productos.Add(new Producto(4, "Filtro aceite", 8000, 5, 1, 2, 1));
                databaseContext.Productos.Add(new Producto(5, "Bujia", 3000, 24, 4, 1, 1));

                await databaseContext.SaveChangesAsync();


            }

            return databaseContext;
        }

        [Fact]
        public async void ProductosService_GetProductos_ReturnsProductos()
        {
            //Arrange
            var dbContext = await GetDb();
            var productosService = new ProductoService(dbContext);
            //Act
            var result = productosService.GetProductos();
            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(5);
            result.Should().BeOfType<List<ProductoDto>>();

        }
        [Fact]
        public async void ProductosService_GetProducto_ReturnProducto()
        {
            //Arrange
            var dbContext = await GetDb();
            var productosService = new ProductoService(dbContext);
            string nombre = "Bujia";
            ProductoDto prod = new("Bujia", 3000, 24, 4, "Bosch", "Unidad");
            //Act
            var result = productosService.GetProducto(nombre);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ProductoDto>();
            result.Should().BeEquivalentTo(prod);
        }

        [Fact]
        public async void ProductosService_UpdateProducto_ReturnProductoUpdated()
        {
            //Arrange
            var dbContext = await GetDb();
            var productosService = new ProductoService(dbContext);
            var newProd = new ProductoDto("Filtro agua", 15000, 50, 5, "NGK", "Unidad");
            int id = 2;
            //Act
            var result = productosService.UpdateProducto(id, newProd);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ProductoDto>();
            result.Should().BeEquivalentTo(newProd);
        }

        [Fact]
        public async void ProductosService_DeleteProducto_ReturnTrue()
        {
            //Arrange
            var dbContext = await GetDb();
            var productosService = new ProductoService(dbContext);
            int id = 4;
            //Act
            var result = productosService.DeleteProducto(id);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void ProductosService_DeleteProducto_ReturnFalse()
        {
            //Arrange
            var dbContext = await GetDb();
            var productosService = new ProductoService(dbContext);
            int id = 10;
            //Act
            var result = productosService.DeleteProducto(id);
            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void ProductosService_SaveProducto_ReturnProductoSaved()
        {
            //Arrange
            var dbContext = await GetDb();
            var productosService = new ProductoService(dbContext);
            var producto = new ProductoDto("Filtro agua", 15000, 50, 5, "NGK", "Unidad");
            //Act
            var result = productosService.SaveProducto(producto);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(producto);
        }



    }
}

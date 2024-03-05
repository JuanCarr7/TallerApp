using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TallerApi.Data;
using TallerApi.Models;
using TallerApi.Services.Imp;

namespace TestTaller.Services
{
    public class UnidadServiceTest
    {
        private async Task<TallerMecanicoPruebaContext> GetDb()
        {
            var options = new DbContextOptionsBuilder<TallerMecanicoPruebaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new TallerMecanicoPruebaContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Productos.CountAsync() <= 0)
            {
                databaseContext.Unidades.Add(new Unidade(1, "Unidad"));
                databaseContext.Unidades.Add(new Unidade(2, "Litros"));
                databaseContext.Marcas.Add(new Marca(1, "Bosch"));
                databaseContext.Marcas.Add(new Marca(2, "NGK"));
                databaseContext.Marcas.Add(new Marca(3, "Castol"));
                databaseContext.Productos.Add(new Producto(1, "Filtro aceite", 10000, 10, 2, 1, 1));
                databaseContext.Productos.Add(new Producto(2, "Filtro aire", 20000, 20, 5, 2, 1));
                databaseContext.Productos.Add(new Producto(3, "Aceite automotor", 1000, 10, 1, 3, 2));
                databaseContext.Productos.Add(new Producto(4, "Filtro aceite", 8000, 5, 1, 2, 1));
                databaseContext.Productos.Add(new Producto(5, "Bujia", 3000, 24, 4, 1, 1));

                await databaseContext.SaveChangesAsync();


            }
            return databaseContext;
        }

        [Fact]
        public async void UnidadService_GetUnidades_ReturnUnidades()
        {
            //Arrange
            var db = await GetDb();
            var unidadService = new UnidadService(db);
            
            //Act
            var result = unidadService.GetUnidades();
            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

        }
        [Fact]
        public async void UnidadService_GetUnidad_ReturnUnidad()
        {
            //Arrange
            var db = await GetDb();
            var unidadService = new UnidadService(db);
            int id = 1;
            string expectedResult = "Unidad";
            //Act
            var result = unidadService.GetUnidad(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }
        [Fact]
        public async void UnidadService_SaveUnidad_ReturnUnidadSaved()
        {
            //Arrange
            var db = await GetDb();
            var unidadService = new UnidadService(db);
            string unidad = "Kilogramos";
            //Act
            var result = unidadService.SaveUnidad(unidad);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(unidad);
        }
        [Fact]
        public async void UnidadService_UpdateUnidad_ReturnUnidadUpdated()
        {
            //Arrange
            var db = await GetDb();
            var unidadService = new UnidadService(db);
            string unidad = "Kilogramos";
            //Act
            var result = unidadService.UpdateUnidad(2,unidad);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(unidad);
        }

        [Fact]
        public async void UnidadService_DeleteUnidad_ReturnTrue()
        {
            //Arrange
            var db = await GetDb();
            var unidadService = new UnidadService(db);
            int id = 1;
            //Act
            var result = unidadService.DeleteUnidad(id);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void UnidadService_DeleteUnidad_ReturnFalse()
        {
            //Arrange
            var db = await GetDb();
            var unidadService = new UnidadService(db);
            int id = 5;
            //Act
            var result = unidadService.DeleteUnidad(id);
            //Assert
            result.Should().BeFalse();
        }
    }
}

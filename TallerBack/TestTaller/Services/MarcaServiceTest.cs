using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerApi.Data;
using TallerApi.Models;
using TallerApi.Services.Imp;

namespace TestTaller.Services
{
    public class MarcaServiceTest
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
        public async void MarcaService_GetMarcas_ReturnMarcas()
        {
            //Arrange
            var db = await GetDb();
            var marcaService = new MarcaService(db);
            //Act
            var result = marcaService.GetMarcas();
            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3); 
        }
        [Fact]
        public async void MarcaService_GetMarca_ReturnMarca()
        {
            //Arrange
            var db = await GetDb();
            var marcaService = new MarcaService(db);
            int id = 2;
            string resultExpected = "NGK";
            //Act
            var result = marcaService.GetMarca(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(resultExpected);
        }
        [Fact]
        public async void MarcaService_SaveMarca_ReturnMarcaSaved()
        {
            //Arrange
            var db = await GetDb();
            var marcaService = new MarcaService(db);
            string marca = "Hescher";
            //Act
            var result = marcaService.SaveMarca(marca);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(marca);
        }
        [Fact]
        public async void MarcaService_UpdateMarca_ReturnMarcaUpdated()
        {
            //Arrange
            var db = await GetDb();
            var marcaService = new MarcaService(db);
            int id = 1;
            string newMarca= "Hescher";
            //Act
            var result = marcaService.UpdateMarca(id, newMarca);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(newMarca);
        }
        [Fact]
        public async void MarcaService_DeleteMarca_ReturnTrue()
        {
            //Arrange
            var db = await GetDb();
            var marcaService = new MarcaService(db);
            int id = 1;
            //Act
            var result = marcaService.DeleteMarca(id);
            //Assert
            result.Should().BeTrue();
        }
        [Fact]
        public async void MarcaService_DeleteMarca_ReturnFalse()
        {
            //Arrange
            var db = await GetDb();
            var marcaService = new MarcaService(db);
            int id = 10;
            //Act
            var result = marcaService.DeleteMarca(id);
            //Assert
            result.Should().BeFalse();
        }
    }
}

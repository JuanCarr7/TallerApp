using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using TallerApi.Controllers;
using TallerApi.Dtos;
using TallerApi.Models;
using TallerApi.Services;

namespace TestTaller.Controllers
{
    public class TallerControllerTest
    {
        private readonly IProductoService _productoService;
        private readonly IMarcaService _marcaService;
        private readonly IUnidadService _unidadService;


        public TallerControllerTest()
        {
            _productoService = A.Fake<IProductoService>();
            _marcaService = A.Fake<IMarcaService>();
            _unidadService = A.Fake<IUnidadService>();

        }

        [Fact]
        public void ProductosController_GetProductos_ReturnOk()
        {
            //Arrange - go get your your variables, functions, classes and put in here

            var Productos = A.Fake<List<Producto>>();
            var controller = new ProductosController(_productoService,_marcaService, _unidadService);


            //Act - execute the function

            var result = controller.getProductos();



            //Assert - Whatever is returned, is it what you want
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));

        }

        [Fact]
        public void ProductosController_SaveProducto_ReturnCreated()
        {
            //Arrange
            var producto = A.Fake<ProductoDto>();
            var controller = new ProductosController(_productoService, _marcaService, _unidadService);

            //Act
            var result = controller.SaveProducto(producto);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(CreatedAtActionResult));
        }

        [Fact]
        public void ProductosController_SaveProducto_ReturnInternalServerError()
        {
            //Arrange
            var producto = A.Fake<ProductoDto>();
            producto = null;
            var controller = new ProductosController(_productoService, _marcaService, _unidadService);

            //Act
            var result = controller.SaveProducto(producto);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ObjectResult));
            
        }
    }
}
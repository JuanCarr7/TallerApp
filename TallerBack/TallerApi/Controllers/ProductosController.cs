using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TallerApi.Dtos;
using TallerApi.Models;
using TallerApi.Services;

namespace TallerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService productoService;
        private readonly IMarcaService marcaService;
        private readonly IUnidadService unidadService;

        public ProductosController(IProductoService producto, IMarcaService marca, IUnidadService unidad)
        {
            productoService = producto;
            marcaService = marca;
            unidadService = unidad;
        }


        //CRUD PRODUCTOS
        [HttpGet]
        public IActionResult getProductos()
        {
            List<ProductoDto> prod = productoService.GetProductos();
            if (prod.IsNullOrEmpty())
            {
                return NotFound("No se han encontrado productos en la base de datos!");
            }
            return Ok(prod);


        }

        [HttpGet("buscar")]
        public IActionResult getProductoByName(string nombre)
        {
            ProductoDto prod = productoService.GetProducto(nombre);
            if (!prod.Equals(null))
            {
                return NotFound("No se ha encontrado el producto deseado.");
            }
            return Ok(prod);

        }

        [HttpPost]
        public IActionResult SaveProducto(ProductoDto producto)
        {
            ProductoDto pResp = productoService.SaveProducto(producto);
            if (pResp != null)
            {
                return CreatedAtAction(nameof(SaveProducto), pResp);
            }
            return StatusCode(500, pResp);
        }

        [HttpDelete("{idProducto}")]
        public IActionResult DeleteProducto(int idProducto)
        {
            bool resp = productoService.DeleteProducto(idProducto);
            if (resp == true)
            {
                return Ok("Se ha eliminado el producto deseado.");
            }
            return BadRequest("No se ha encontrado el producto");
        }

        [HttpPut("{idProducto}")]
        public IActionResult UpdateProducto(int idProducto, ProductoDto producto)
        {
            ProductoDto resp = productoService.UpdateProducto(idProducto, producto);
            if (resp != null)
            {
                return Ok(resp);

            }
            return BadRequest("No se ha encontrado el producto");
        }

        
        //CRUD MARCAS
        [HttpGet("marcas")]
        public IActionResult GetMarcas()
        {
            List<string> marcas = marcaService.GetMarcas();
            if (marcas.IsNullOrEmpty())
            {
                return BadRequest("No se han encontrado marcas.");
            }
            return Ok(marcas);

        }
        [HttpGet("marca/{id}")]
        public IActionResult GetMarca(int id)
        {
            string m = marcaService.GetMarca(id);
            if (m == "") { return BadRequest("No se ha encontrado una marca!"); }
            return Ok(m);
        }
        [HttpPost("marca")]
        public IActionResult SaveMarca(string marca)
        {
            string m = marcaService.SaveMarca(marca);
            if (m == "") { return StatusCode(500, m); }
            return CreatedAtAction("SaveMarca",m);
        }

        [HttpDelete("marca/{id}")]
        public IActionResult DeleteMarcas(int id)
        {
            var resp = marcaService.DeleteMarca(id);
            
            if(resp){
                return Ok("Se ha eliminado la marca seleccionada");
            }
            return BadRequest("No se ha borrado la marca. Intente de nuevo luego!");
        }

        [HttpPut("marca")]
        public IActionResult UpdateMarcas(int id, string marca){
            string marcaRespone = marcaService.UpdateMarca(id, marca);
            if(marcaRespone != null){
                return Ok(marcaRespone);
            }
            return BadRequest(marcaRespone);

        }

        //CRUD UNIDADES

        [HttpGet("unidades")]
        public IActionResult GetUnidades(){
            List<string> response = unidadService.GetUnidades();
            if(response.IsNullOrEmpty()){
                return BadRequest("No se han encontrado unidades que listar!");
            }
            return Ok(response);
        }
        [HttpGet("unidades/{id}")]
        public IActionResult GetUnidad(int id){
            string? resp = unidadService.GetUnidad(id);
            if(resp==null || resp == ""){
                return BadRequest("No se ha encontrado o no existe la unidad buscada.");
            }
            return Ok(resp);
        }
        [HttpPost("unidades")]
        public IActionResult SaveUnidad(string unidad){
            string? resp = unidadService.SaveUnidad(unidad);
            if(resp==null || resp == ""){
                return BadRequest("No se ha encontrado o no existe la unidad buscada.");
            }
            return Ok(resp);
        }
        [HttpPut("unidades/{id}")]
        public IActionResult UpdateUnidad(int id, string unidad){
            string? resp = unidadService.UpdateUnidad(id, unidad);
            if(resp==null || resp == ""){
                return BadRequest("No se ha encontrado o no existe la unidad buscada.");
            }
            return Ok(resp);
        }
        [HttpDelete("unidades/{id}")]
        public IActionResult  DeleteUnidad(int id){
             bool resp = unidadService.DeleteUnidad(id);
            if(resp==false){
                return BadRequest("No se ha encontrado o no existe la unidad a borrar.");
            }
            return Ok("Se ha borrado con exito la unidad!");
        }
    }
}
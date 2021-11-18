using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoFinal.Helpers;
using ProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        // GET: api/<ProductosController>
        [HttpGet("Todos")]
        public ActionResult Get()
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            try
            {
                if (db.productos != null)
                {
                    result.Datos = db.productos.ToList();
                }
                else
                {
                    throw new Exceptions("No hay PRODUCTOS para mostrar!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet("SoloProductosActivos")]
        public ActionResult SoloProductosActivos()
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            var ListaProductos = from p in db.productos where p.Estado == "Activo" orderby p.IDCategoria select p;
            try
            {
                if (ListaProductos != null)
                {
                    result.Datos = ListaProductos;
                }
                else
                {
                    throw new Exceptions("No se encontraron Productos activos!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // GET api/<ProductosController>/5
        [HttpGet("BuscarPor/{id}")]
        public ActionResult Get(int id)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Producto BuscarProducto = db.productos.Find(id);
            try
            {
                if (BuscarProducto != null)
                {
                    result.Datos = BuscarProducto;
                }
                else
                {
                    throw new Exceptions("El producto que busca no existe");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // POST api/<ProductosController>
        [HttpPost("NuevoProducto")]
        public ActionResult Post([FromBody] Producto p)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Producto BuscarProducto, NuevoProducto;
            BuscarProducto = db.productos.Find(p.ID);
            try
            {
                if (BuscarProducto != null)
                {
                    throw new Exceptions("El ID proporcionado ya esta en USO!!!");
                }
                else
                {
                    NuevoProducto = new Producto(p.IDCategoria, p.Codigo, p.Nombre, p.PrecioVenta, p.Existencia, p.Descripcion, p.Estado);
                    db.productos.Add(NuevoProducto);
                    db.SaveChanges();
                    result.Datos = NuevoProducto;
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // PUT api/<ProductosController>/5
        [HttpPut("ActualizarProducto/{id}")]
        public ActionResult Put(int id, [FromBody] Producto p)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Producto BuscarProducto, ActualizarProducto;
            BuscarProducto = db.productos.Find(id);
            try
            {
                if (BuscarProducto == null)
                {
                    throw new Exceptions("No existe el Producto!!!");
                }
                ActualizarProducto = db.productos.Find(id);
                if (ActualizarProducto != null)
                {
                    ActualizarProducto.IDCategoria = p.IDCategoria;
                    ActualizarProducto.Codigo = p.Codigo;
                    ActualizarProducto.Nombre = p.Nombre;
                    ActualizarProducto.PrecioVenta = p.PrecioVenta;
                    ActualizarProducto.Existencia = p.Existencia;
                    ActualizarProducto.Descripcion = p.Descripcion;
                    ActualizarProducto.Estado = p.Estado;
                    db.Entry(ActualizarProducto).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = ActualizarProducto;
                }
                else
                {
                    throw new Exceptions("Error al actualizar Datos");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // DELETE api/<ProductosController>/5
        [HttpDelete("EliminarProducto/{id}")]
        public ActionResult Delete(int id, [FromBody] Producto p)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Producto BuscarProducto = db.productos.Find(id);
            try
            {
                if (BuscarProducto != null)
                {
                    BuscarProducto.Estado = (p.Estado = "Baja");
                    db.Entry(BuscarProducto).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = BuscarProducto;
                }
                else
                {
                    throw new Exceptions("El ID que proporciono no existe");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
    }
}

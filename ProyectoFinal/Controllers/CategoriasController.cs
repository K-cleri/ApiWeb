using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoFinal.Models;
using ProyectoFinal.Helpers;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        // GET: api/<CategoriasController>
        [HttpGet("Todos")]
        public ActionResult  Get()
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            try
            {
                if (db.categorias != null)
                {
                    result.Datos = db.categorias.ToList();
                }
                else
                {
                    throw new Exceptions("No hay categorias para mostrar!!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet("SoloCategoriasActivas")]
        public ActionResult SoloCategoriasActivas()
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            var ListaCategorias = from c in db.categorias where c.Estado == "Activo" orderby c.ID select c;
            try
            {
                if (ListaCategorias != null)
                {
                    result.Datos = ListaCategorias;
                }
                else
                {
                    throw new Exceptions("No hay Categorias Activas");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // GET api/<CategoriasController>/5
        [HttpGet("BuscarPor/{id}")]
        public ActionResult Get(int id)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Categoria BuscarCategoria = db.categorias.Find(id);
            try
            {
                if (BuscarCategoria != null)
                {
                    result.Datos = BuscarCategoria;
                }
                else
                {
                    throw new Exceptions("No se encontro la CATEGORIA que desea!!!");
                }
            }
            catch (Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // POST api/<CategoriasController>
        [HttpPost("NuevaCategoria")]
        public ActionResult Post([FromBody] Categoria c)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Categoria BuscarCategoria, NuevaCategoria;
            BuscarCategoria = db.categorias.Find(c.ID);
            try
            {
                if (BuscarCategoria != null)
                {
                    throw new Exceptions("El ID proporcionado ya esta en USO!!!");
                }
                else
                {
                    NuevaCategoria = new Categoria(c.Nombre, c.Descripcion, c.Estado = "Activo");
                    db.categorias.Add(NuevaCategoria);
                    db.SaveChanges();
                    result.Datos = NuevaCategoria;
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // PUT api/<CategoriasController>/5
        [HttpPut("ActualizarCategoria/{id}")]
        public ActionResult Put(int id, [FromBody] Categoria c)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Categoria BuscarCategoria, ActualizarCategoria;
            BuscarCategoria = db.categorias.Find(id);
            try
            {
                if (BuscarCategoria == null)
                {
                    throw new Exceptions("No existe la Categoria que proporciono!!!");
                }
                ActualizarCategoria = db.categorias.Find(id);
                if (ActualizarCategoria != null)
                {
                    ActualizarCategoria.Nombre = c.Nombre;
                    ActualizarCategoria.Descripcion = c.Descripcion;
                    ActualizarCategoria.Estado = c.Estado;
                    db.Entry(ActualizarCategoria).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = ActualizarCategoria;
                }
                else
                {
                    throw new Exceptions("Error al actualizar los datos!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // DELETE api/<CategoriasController>/5
        [HttpDelete("EliminarCategoria/{id}")]
        public ActionResult Delete(int id, [FromBody] Categoria c)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Categoria BuscarCategoria = db.categorias.Find(id);
            try
            {
                if (BuscarCategoria != null)
                {
                    result.Datos = BuscarCategoria;
                }
                else
                {
                    throw new Exceptions("La categoria No existe!!!");
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

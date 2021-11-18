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
    public class ClientesController : ControllerBase
    {
        // GET: api/<ClientesController>
        [HttpGet("Todos")]
        public ActionResult Get()
        {
            DatosDB db = new DatosDB();
            Results result = new Results();
            try
            {
                if (db.clientes != null)
                {
                    result.Datos = db.clientes.ToList();
                }
                else
                {
                    throw new Exceptions("No hay CLIENTES para mostrar!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        //Get por Clientes Activos
        [HttpGet("SoloClientesActivos")]
        public ActionResult SoloClientesActivos()
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            var ListaClientes = from c in db.clientes where c.Estado == "Activo" orderby c.ID select c;
            try
            {
                if (ListaClientes != null)
                {
                    result.Datos = ListaClientes;
                }
                else
                {
                    throw new Exceptions("No hay datos para Mostrar");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // GET api/<ClientesController>/5
        [HttpGet("BuscarPor/{id}")]
        public ActionResult Get(int id)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Cliente BuscarCliente;
            BuscarCliente = db.clientes.Find(id);
            try
            {
                if (BuscarCliente != null)
                {
                    result.Datos = BuscarCliente;
                }
                else
                {
                    throw new Exceptions("No se encontro el CLIENTE deseado!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // POST api/<ClientesController>
        [HttpPost("NuevoCliente")]
        public ActionResult Post([FromBody] Cliente c)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Cliente BuscarCliente, NuevoCliente;
            BuscarCliente = db.clientes.Find(c.ID);
            try
            {
                if (BuscarCliente != null)
                {
                    throw new Exceptions("El ID proporcionado ya esta en USO!!!");
                }
                else
                {
                    NuevoCliente = new Cliente(c.Nombre, c.Direccion, c.Telefono, c.Email, c.Rfc, c.Estado = "Activo");
                    db.clientes.Add(NuevoCliente);
                    db.SaveChanges();
                    result.Datos = NuevoCliente;
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        //PUT api/<ClientesController>/5
        [HttpPut("ActualizarCliente/{id}")]
        public ActionResult Put(int id, [FromBody] Cliente c)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Cliente BuscarCliente, ActualizarCliente;
            BuscarCliente = db.clientes.Find(id);
            try
            {
                if (BuscarCliente == null)
                {
                    throw new Exceptions("No existe este CLIENTE!!!");
                }
                ActualizarCliente = db.clientes.Find(id);
                if (ActualizarCliente != null)
                {
                    ActualizarCliente.Nombre = c.Nombre;
                    ActualizarCliente.Direccion = c.Direccion;
                    ActualizarCliente.Telefono = c.Telefono;
                    ActualizarCliente.Email = c.Email;
                    ActualizarCliente.Rfc = c.Rfc;
                    ActualizarCliente.Estado = c.Estado;
                    db.Entry(ActualizarCliente).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = ActualizarCliente;
                }
                else
                {
                    throw new Exceptions("Error al actualizar datos!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // DELETE api/<ClientesController>/5
        [HttpDelete("EliminarCliente/{id}")]
        public ActionResult Delete(int id, [FromBody] Cliente c)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Cliente BuscarCliente;
            BuscarCliente = db.clientes.Find(id);
            try
            {
                if (BuscarCliente != null)
                {
                    BuscarCliente.Estado = c.Estado = "Baja";
                    db.Entry(BuscarCliente).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = BuscarCliente;
                }
                else
                {
                    throw new Exceptions("Error, No existe el Cliente!!!");
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

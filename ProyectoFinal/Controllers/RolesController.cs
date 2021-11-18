using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ProyectoFinal.Helpers;
using ProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        // GET: api/<RolesController>
        [HttpGet("Todos")]
        public ActionResult Get()
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            try
            {
                if (db.roles != null)
                {
                    result.Datos = db.roles.ToList();
                }
                else
                {
                    throw new Exceptions("No hay ROLES que mostrar!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet("SoloRolesActivos")]
        public ActionResult SoloRolesActivos()
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            var ListaRoles = from r in db.roles where r.Estado == "Activo" orderby r.ID select r;
            try
            {
                if (ListaRoles != null)
                {
                    result.Datos = ListaRoles;
                }
                else
                {
                    throw new Exceptions("No hay Roles Activos!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // GET api/<RolesController>/5
        [HttpGet("BuscarPor/{id}")]
        public ActionResult Get(int id)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Rol BuscarRol = db.roles.Find(id);
            try
            {
                if (BuscarRol != null)
                {
                    result.Datos = BuscarRol;
                }
                else
                {
                    throw new Exceptions("No se encontro el Rol que Busca!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // POST api/<RolesController>
        [HttpPost("NuevoRol")]
        public ActionResult Post([FromBody] Rol r)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Rol BuscarRol, NuevoRol;
            BuscarRol = db.roles.Find(r.ID);
            try
            {
                if (BuscarRol != null)
                {
                    throw new Exceptions("El ID que Proporciono esta en USO!!!");
                }
                else
                {
                    NuevoRol = new Rol(r.Nombre, r.Descripcion, r.Estado = "Activo");
                    db.roles.Add(NuevoRol);
                    db.SaveChanges();
                    result.Datos = NuevoRol;
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // PUT api/<RolesController>/5
        [HttpPut("ActualizarRol/{id}")]
        public ActionResult Put(int id, [FromBody] Rol r)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Rol BuscarRol, ActualizarRol;
            BuscarRol = db.roles.Find(id);
            try
            {
                if(BuscarRol == null)
                {
                    throw new Exceptions("El Rol no EXISTE!!!");
                }
                ActualizarRol = db.roles.Find(id);
                if (ActualizarRol != null)
                {
                    ActualizarRol.Nombre = r.Nombre;
                    ActualizarRol.Descripcion = r.Descripcion;
                    ActualizarRol.Estado = r.Estado;
                    db.Entry(ActualizarRol).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = ActualizarRol;
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("EliminarRol/{id}")]
        public ActionResult Delete(int id, [FromBody] Rol r)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Rol BuscarRol = db.roles.Find(id);
            try
            {
                if (BuscarRol != null)
                {
                    BuscarRol.Estado = (r.Estado = "Baja");
                    db.Entry(BuscarRol).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = BuscarRol;
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

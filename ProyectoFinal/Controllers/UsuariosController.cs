using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ProyectoFinal.Models;
using ProyectoFinal.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        // GET: api/<UsuariosController>
        [HttpGet("Todos")]
        public ActionResult Get()
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            try
            {
                if (db.usuarios != null)
                {
                    result.Datos = db.usuarios.ToList();
                }
                else
                {
                    throw new Exceptions("No hay USUARIOS para mostrar!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet("SoloUsuariosActivos")]
        public ActionResult SoloUsuariosActivos()
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            var ListaUsuarios = from u in db.usuarios where u.Estado == "Activo" orderby u.IDRol select u;
            try
            {
                if (ListaUsuarios != null)
                {
                    result.Datos = ListaUsuarios;
                }
                else
                {
                    throw new Exceptions("No hay Usuarios Activos");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // GET api/<UsuariosController>/5
        [HttpGet("BuscarPor/{id}")]
        public ActionResult Get(int id)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Usuario BuscarUsuario = db.usuarios.Find(id);
            try
            {
                if (BuscarUsuario != null)
                {
                    result.Datos = BuscarUsuario;
                }
                else
                {
                    throw new Exceptions("El Usuario que busca no se encontro!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // POST api/<UsuariosController>
        [HttpPost("NuevoUsuario")]
        public ActionResult Post([FromBody] Usuario u)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Usuario BuscarUsuario, NuevoUsuario;
            BuscarUsuario = db.usuarios.Find(u.ID);
            try
            {
                if (BuscarUsuario != null)
                {
                    throw new Exceptions("El ID ya esta en USO!!!");
                }
                else
                {
                    NuevoUsuario = new Usuario(u.IDRol, u.Nombre, u.Direccion, u.Telefono, u.Email, u.Estado = "Activo");
                    db.usuarios.Add(NuevoUsuario);
                    db.SaveChanges();
                    result.Datos = NuevoUsuario;
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // PUT api/<UsuariosController>/5
        [HttpPut("ActualizarUsuario/{id}")]
        public ActionResult Put(int id, [FromBody] Usuario u)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Usuario BuscarUsuario, ActuializarUsuario;
            BuscarUsuario = db.usuarios.Find(id);
            try
            {
                if(BuscarUsuario == null)
                {
                    throw new Exceptions("No existe este USUARIO!!!");
                }
                ActuializarUsuario = db.usuarios.Find(id);
                if (ActuializarUsuario != null)
                {
                    ActuializarUsuario.IDRol = u.IDRol;
                    ActuializarUsuario.Nombre = u.Nombre;
                    ActuializarUsuario.Direccion = u.Direccion;
                    ActuializarUsuario.Telefono = u.Telefono;
                    ActuializarUsuario.Email = u.Email;
                    ActuializarUsuario.Estado = u.Estado;
                    db.Entry(ActuializarUsuario).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = ActuializarUsuario;
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // DELETE api/<UsuariosController>/5
        [HttpDelete("EliminarUsuario/{id}")]
        public ActionResult Delete(int id, [FromBody] Usuario u)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Usuario BuscarUsuario = db.usuarios.Find(id);
            try
            {
                if (BuscarUsuario != null)
                {
                    BuscarUsuario.Estado = u.Estado = "Baja";
                    db.Entry(BuscarUsuario).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = BuscarUsuario;
                }
                else
                {
                    throw new Exceptions("Error, No existe este USUARIO!!!");
                }
            }
            catch (Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
    }
}

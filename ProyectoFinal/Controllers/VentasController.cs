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
    public class VentasController : ControllerBase
    {
        // GET: api/<VentasController>
        [HttpGet("Todos")]
        public ActionResult Get()
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            try
            {
                if (db.ventas != null)
                {
                    result.Datos = db.ventas.ToList();
                }
                else
                {
                    throw new Exceptions("No hay VENTAS para mostrar!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        //Get Todos Por ID del Cliente
        [HttpGet("TodosPorIDDelCliente")]
        public ActionResult TodosOrdenadosPorIDCliente()
        {
            DatosDB db = new DatosDB();
            Results result = new Results();
            var ListaVentas = from c in db.ventas orderby c.IDCliente select c;
            try
            {
                if (ListaVentas != null)
                {
                    result.Datos = ListaVentas;
                }
                else
                {
                    throw new Exceptions("No hay datos en la base de datos");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // GET api/<VentasController>/5
        [HttpGet("Buscar/{id}")]
        public ActionResult Get(int id)
        {
            DatosDB db = new DatosDB();
            Results result = new Results();
            Venta BuscarVenta;
            BuscarVenta = db.ventas.Find(id);
            try
            {
                if(BuscarVenta != null)
                {
                    result.Datos = BuscarVenta;
                }
                else
                {
                    throw new Exceptions("No se encontro la VENTA que indica!!!");
                }
            }
            catch (Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }

        // POST api/<VentasController>
        [HttpPost("NuevaVenta")]
        public ActionResult Post([FromBody] Venta v)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Venta NuevaVenta, BuscarVenta;
            BuscarVenta = db.ventas.Find(v.ID);
            try
            {
                if (BuscarVenta != null)
                {
                    throw new Exception("El ID proporcionado ya esta en USO!!!");
                }
                else
                {
                    NuevaVenta = new Venta(v.IDCliente , v.IDUsuario, v.NumFactura, v.FechaHora = DateTime.Now, v.Impuesto, v.Total);
                    db.ventas.Add(NuevaVenta);
                    db.SaveChanges();
                    result.Datos = NuevaVenta;
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }

        // PUT api/<VentasController>/5
        [HttpPut("ActualizarVenta/{id}")]
        public ActionResult Put(int id, [FromBody] Venta v)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Venta BuscarVenta, ActualizarVenta;
            BuscarVenta = db.ventas.Find(id);
            try
            {
                if (BuscarVenta == null)
                {
                    throw new Exceptions("No existe esa VENTA!!!");
                }
                ActualizarVenta = db.ventas.Find(id);
                if(ActualizarVenta != null)
                {
                    ActualizarVenta.IDCliente = v.IDCliente;
                    ActualizarVenta.IDUsuario = v.IDUsuario;
                    ActualizarVenta.NumFactura = v.NumFactura;
                    ActualizarVenta.FechaHora = (v.FechaHora = DateTime.Now);
                    ActualizarVenta.Impuesto = v.Impuesto;
                    ActualizarVenta.Total = v.Total;
                    db.Entry(ActualizarVenta).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = ActualizarVenta;
                }
                else
                {
                    throw new Exceptions("El ID que proporciono ya esta en USO!!!");
                }
            }
            catch(Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }

        // DELETE api/<VentasController>/5
        [HttpDelete("EliminarVenta/{id}")]
        public ActionResult Delete(int id)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            Venta BuscarVenta;
            BuscarVenta = db.ventas.Find(id);
            try
            {
                if (BuscarVenta == null)
                {
                    throw new Exceptions("La venta NO existe!!!");
                }
                else
                {
                    db.ventas.Remove(BuscarVenta);
                    db.SaveChanges();
                    result.Datos = BuscarVenta;
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

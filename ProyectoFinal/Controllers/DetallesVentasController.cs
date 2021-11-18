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
    public class DetallesVentasController : ControllerBase
    {
        // GET: api/<DetallesVentasController>
        [HttpGet("Todos")]
        public ActionResult Get()
        {
            DatosDB db = new DatosDB();
            Results result = new Results();
            try
            {
                if (db.detallesventas != null)
                {
                    result.Datos = db.detallesventas.ToList();
                }
                else
                {
                    throw new Exceptions("No hay Detlles de Ventas para mostrar!!!");
                }
            }
            catch (Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        [HttpGet("SoloDetallesVentasVendidos")]
        public ActionResult SoloDetallesVentasVendidos()
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            var ListaDetallesVenta = from d in db.detallesventas where d.Estado == "Vendido" orderby d.IDVenta select d;
            try
            {
                if (ListaDetallesVenta != null)
                {
                    result.Datos = ListaDetallesVenta;
                }
                else
                {
                    throw new Exceptions("Error, No hay datos para mostrar");
                }
            }
            catch (Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // GET api/<DetallesVentasController>/5
        [HttpGet("BuscarPor/{id}")]
        public ActionResult Get(int id)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            DetallesVenta BuscarDetallesVenta = db.detallesventas.Find(id);
            try
            {
                if (BuscarDetallesVenta != null)
                {
                    result.Datos = BuscarDetallesVenta;
                }
                else
                {
                    throw new Exceptions("No se encontraron los detalles de la venta que indico!!!");
                }
            }
            catch (Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // POST api/<DetallesVentasController>
        [HttpPost("NuevosDetallesVentas")]
        public ActionResult Post([FromBody] DetallesVenta d)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            DetallesVenta BuscarDetallesVenta, NuevosDetallesVenta;
            BuscarDetallesVenta = db.detallesventas.Find(d.ID);
            try
            {
                if (BuscarDetallesVenta != null)
                {
                    throw new Exceptions("El ID proporcionado ya esta en USO!!!");
                }
                else
                {
                    NuevosDetallesVenta = new DetallesVenta(d.IDVenta, d.IDProducto, d.Cantidad, d.Precio, d.Descuento, d.Estado = "Vendido");
                    db.detallesventas.Add(NuevosDetallesVenta);
                    db.SaveChanges();
                    result.Datos = NuevosDetallesVenta;
                }
            }
            catch (Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // PUT api/<DetallesVentasController>/5
        [HttpPut("ActualizarDetallesVenta/{id}")]
        public ActionResult Put(int id, [FromBody] DetallesVenta d)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            DetallesVenta BuscarDetallesVenta, ActualizarDetallesVenta;
            BuscarDetallesVenta = db.detallesventas.Find(id);
            try
            {
                if (BuscarDetallesVenta == null)
                {
                    throw new Exceptions("No existen los detalles de venta");
                }
                ActualizarDetallesVenta = db.detallesventas.Find(id);
                if (ActualizarDetallesVenta != null)
                {
                    ActualizarDetallesVenta.IDVenta = d.IDVenta;
                    ActualizarDetallesVenta.IDProducto = d.IDProducto;
                    ActualizarDetallesVenta.Cantidad = d.Cantidad;
                    ActualizarDetallesVenta.Precio = d.Precio;
                    ActualizarDetallesVenta.Descuento = d.Descuento;
                    ActualizarDetallesVenta.Estado = d.Estado;
                    db.Entry(ActualizarDetallesVenta).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = ActualizarDetallesVenta;
                }
                else
                {
                    throw new Exceptions("Error al actualizar datos");
                }
            }
            catch (Exceptions ex)
            {
                result.Estado = false;
                result.Mensaje = ex.Message;
            }
            return Ok(result);
        }
        // DELETE api/<DetallesVentasController>/5
        [HttpDelete("EliminarDetallesVenta/{id}")]
        public ActionResult Delete(int id, [FromBody] DetallesVenta d)
        {
            Results result = new Results();
            DatosDB db = new DatosDB();
            DetallesVenta BuscarDetallesVenta = db.detallesventas.Find(id);
            try
            {
                if (BuscarDetallesVenta != null)
                {
                    BuscarDetallesVenta.Estado = (d.Estado = "Cancelado");
                    db.Entry(BuscarDetallesVenta).State = EntityState.Modified;
                    db.SaveChanges();
                    result.Datos = BuscarDetallesVenta;
                }
                else
                {
                    throw new Exceptions("No existe el ID que proporciono");
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

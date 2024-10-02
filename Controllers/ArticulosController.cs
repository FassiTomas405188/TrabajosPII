using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica02.Models;

namespace Practica02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private static List<Factura> facturas = new List<Factura>();

        [HttpGet]
        public ActionResult<IEnumerable<Factura>> GetFacturas()
        {
            return Ok(facturas);
        }

        [HttpPost]
        public ActionResult<Factura> PostFactura([FromBody] Factura factura)
        {
            if (factura == null)
            {
                return BadRequest("Factura is null.");
            }

            factura.Nro = facturas.Count + 1; 
            facturas.Add(factura);

            return CreatedAtAction(nameof(GetFacturas), new { id = factura.Nro }, factura);
        }
    }
}

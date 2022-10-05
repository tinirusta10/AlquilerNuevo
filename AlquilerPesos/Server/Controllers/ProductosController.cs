using Alquiler.BD.Data;
using Alquiler.BD.Data.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlquilerPesos.Server.Controllers
{
    [Route("api/Productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly Bdcontext context;

        public ProductosController(Bdcontext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            var resp = await context.Productos.ToListAsync();
            return resp;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var venta = await context.Productos
                                         .Where(e => e.Id == id)
                                         .Include(m => m.Fotos)
                                         .FirstOrDefaultAsync();

            if (venta == null)
            {
                return NotFound($"No existe Producto de id: {id}");
            }
            return venta;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(Producto producto)
        {
            try
            {

                context.Productos.Add(producto);
                await context.SaveChangesAsync();
                return producto.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Producto producto)
        {


            if (id != producto.Id)
            {
                return BadRequest("No existe el Producto");
            }

            var produ = context.Productos.Where(e => e.Id == id).FirstOrDefault();
            var fotoss = context.Fotos.Where(e => e.Id == id).FirstOrDefault();

            if (produ == null)
            {
                return NotFound("No existe el Producto");
            }

            produ.NombreProducto = producto.NombreProducto;
            produ.PrecioProducto = producto.PrecioProducto;
            produ.DetallesProducto = producto.DetallesProducto;

            try
            {
                //throw(new Exception("Cualquier Verdura"));
                context.Productos.Update(produ);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no han sido actualizados por: {e.Message}");
            }
        }



        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produ = context.Productos.Where(x => x.Id == id).FirstOrDefault();

            if (produ == null)
            {
                return NotFound($"El registro {id} no fue encontrado");
            }

            try
            {
                context.Productos.Remove(produ);
                context.SaveChanges();
                return Ok($"El registro de {produ.NombreProducto} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no pudieron eliminarse por: {e.Message}");
            }
        }
    }
}

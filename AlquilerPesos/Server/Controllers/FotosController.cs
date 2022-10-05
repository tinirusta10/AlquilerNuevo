using Alquiler.BD.Data;
using Alquiler.BD.Data.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlquilerPesos.Server.Controllers
{
    [ApiController]
    [Route("api/Fotos")]

    public class FotosController : ControllerBase
    {
        private readonly Bdcontext context;

        public FotosController(Bdcontext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Foto>>> Get()
        {

            return await context.Fotos.ToListAsync();


        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Foto>> Get(int id)
        {
            var foto = await context.Fotos

                .Where(e => e.Id == id)
                .Include(m => m.Producto)
               .FirstOrDefaultAsync();


            if (foto == null)

            {
                return NotFound($"No existe la Foto de Id= {id}");

            }

            return foto;

        }


        [HttpPost]

        public async Task<ActionResult<int>> Post(Foto foto)
        {
            try
            {


                context.Fotos.Add(foto);
                await context.SaveChangesAsync();
                return foto.Id;

            }
            catch (Exception m)
            {
                return BadRequest(m.Message);
            }


        }




        [HttpGet("FotosPorNombre/{nombre}")]

        public async Task<ActionResult<Foto>> FotosPorNombre(string nombre)
        {
            var foto = await context.Fotos

             .Where(x => x.Fotos == nombre)
             .Include(m => m.Producto)
             .FirstOrDefaultAsync();


            if (foto == null)

            {
                return NotFound($"No existe la foto = {nombre}");

            }

            return foto;

        }




        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Foto Cargo)
        {


            if (id != Cargo.Id)
            {
                return BadRequest("No existe la Foto");
            }

            var carg = context.Fotos.Where(e => e.Id == id).FirstOrDefault();
            var emplead = context.Productos.Where(e => e.Id == id).FirstOrDefault();
            

            if (carg == null)
            {
                return NotFound("No existe La Foto");
            }

            carg.Fotos = Cargo.Fotos;

            

            try
            {
                //throw(new Exception(""));
                context.Fotos.Update(carg);
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
            var carg = context.Fotos.Where(x => x.Id == id).FirstOrDefault();

            if (carg == null)
            {
                return NotFound($"La Foto {id} no fue encontrado");
            }

            try
            {
                context.Fotos.Remove(carg);
                context.SaveChanges();
                return Ok($"El registro de {carg.Fotos} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no pudieron eliminarse por: {e.Message}");
            }
        }
    }
}
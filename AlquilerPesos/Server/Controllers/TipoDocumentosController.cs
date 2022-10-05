using Alquiler.BD.Data;
using Alquiler.BD.Data.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlquilerPesos.Server.Controllers
{
    [ApiController]
    [Route("api/TipoDocumentos")]

    public class TipoDocumentosController : ControllerBase
    {
        private readonly Bdcontext context;

        public TipoDocumentosController(Bdcontext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoDocumento>>> Get()
        {

            return await context.TipoDocumentos.ToListAsync();


        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoDocumento>> Get(int id)
        {
            var documento = await context.TipoDocumentos

                .Where(e => e.Id == id)
                .Include(m => m.NombreTipoDocumento)
                   .Include(m => m.Personas)
                .FirstOrDefaultAsync();


            if (documento == null)

            {
                return NotFound($"No existe el Tipo Documento de Id= {id}");

            }

            return documento;

        }


        [HttpPost]

        public async Task<ActionResult<int>> Post(TipoDocumento documento)
        {
            try
            {


                context.TipoDocumentos.Add(documento);
                await context.SaveChangesAsync();
                return documento.Id;

            }
            catch (Exception m)
            {
                return BadRequest(m.Message);
            }


        }




        [HttpGet("TipoDocumentosPorNombre/{nombre}")]

        public async Task<ActionResult<TipoDocumento>> TipoDocumentosPorNombre(string nombre)
        {
            var documento = await context.TipoDocumentos

             .Where(x => x.NombreTipoDocumento == nombre)
             .Include(m => m.Personas)
             .FirstOrDefaultAsync();


            if (documento == null)

            {
                return NotFound($"No existe el Tipo Documento = {nombre}");

            }

            return documento;

        }




        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] TipoDocumento Cargo)
        {


            if (id != Cargo.Id)
            {
                return BadRequest("No existe la TipoDocumento");
            }

            var carg = context.TipoDocumentos.Where(e => e.Id == id).FirstOrDefault();
            var emplead = context.Personas.Where(e => e.Id == id).FirstOrDefault();
           

            if (carg == null)
            {
                return NotFound("No existe el TipoDocumento");
            }

            carg.NombreTipoDocumento = Cargo.NombreTipoDocumento;

           


            try
            {
                //throw(new Exception(""));
                context.TipoDocumentos.Update(carg);
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
            var documento = context.TipoDocumentos.Where(x => x.Id == id).FirstOrDefault();

            if (documento == null)
            {
                return NotFound($"El Tipo Documento {id} no fue encontrado");
            }

            try
            {
                context.TipoDocumentos.Remove(documento);
                context.SaveChanges();
                return Ok($"El registro de {documento.NombreTipoDocumento} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no pudieron eliminarse por: {e.Message}");
            }
        }
    }
}
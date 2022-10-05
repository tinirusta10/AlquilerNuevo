using Alquiler.BD.Data;
using Alquiler.BD.Data.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlquilerPesos.Server.Controllers
{
        [ApiController]
        [Route("api/Direcciones")]

        public class DireccionesController : ControllerBase
        {
            private readonly Bdcontext context;

            public DireccionesController(Bdcontext context)
            {
                this.context = context;
            }

            [HttpGet]
            public async Task<ActionResult<List<Direccion>>> Get()
            {

                return await context.Direcciones.ToListAsync();


            }


            [HttpGet("{id:int}")]
            public async Task<ActionResult<Direccion>> Get(int id)
            {
                var direccion = await context.Direcciones

                    .Where(e => e.Id == id)
                    .Include(m => m.Personas)
                    .FirstOrDefaultAsync();


                if (direccion == null)

                {
                    return NotFound($"No existe la Direccion de Id= {id}");

                }

                return direccion;

            }


            [HttpPost]

            public async Task<ActionResult<int>> Post(Direccion direccion)
            {
                try
                {


                    context.Direcciones.Add(direccion);
                    await context.SaveChangesAsync();
                    return direccion.Id;

                }
                catch (Exception m)
                {
                    return BadRequest(m.Message);
                }


            }




            [HttpGet("DireccionesPorLocalidad/{nombre}")]

            public async Task<ActionResult<Direccion>> DireccionesPorLocalidad(string nombre)
            {
                var direccion = await context.Direcciones

                 .Where(x => x.Localidad == nombre)
                 .Include(m => m.Personas)
                 .FirstOrDefaultAsync();


                if (direccion == null)

                {
                    return NotFound($"No existe la direccion = {nombre}");

                }

                return direccion;

            }




            [HttpPut("{id:int}")]
            public ActionResult Put(int id, [FromBody] Direccion Cargo)
            {


                if (id != Cargo.Id)
                {
                    return BadRequest("No existe la Direccion");
                }

                var carg = context.Direcciones.Where(e => e.Id == id).FirstOrDefault();
                var emplead = context.Personas.Where(e => e.Id == id).FirstOrDefault();
               

                if (carg == null)
                {
                    return NotFound("No existe la Direccion");
                }

                carg.Localidad = Cargo.Localidad;

                carg.Departamento = Cargo.Departamento;

                carg.Provincia = Cargo.Provincia;

                



                try
                {
                    //throw(new Exception(""));
                    context.Direcciones.Update(carg);
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
                var carg = context.Direcciones.Where(x => x.Id == id).FirstOrDefault();

                if (carg == null)
                {
                    return NotFound($"La Direccion {id} no fue encontrado");
                }

                try
                {
                    context.Direcciones.Remove(carg);
                    context.SaveChanges();
                    return Ok($"El registro {id} ha sido borrado.");
                }
                catch (Exception e)
                {
                    return BadRequest($"Los datos no pudieron eliminarse por: {e.Message}");
                }
            }
        }
    }
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Foro.Entities;

namespace WebApi_Foro.Controllers
{
    [Route("api/pacientes")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly PacientesDbContext _context;

        public PacientesController(PacientesDbContext context)
        {
            _context = context;
        }

        //Traemos todas las pacientes que tenemos en nuestra DB
        [HttpGet]
        public async Task<ActionResult<List<Paciente>>> Get()
        {
            return await _context.Pacientes.ToListAsync();
        }

        //Buscamos en nuestra DB, un paciente por el id especifico
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Paciente>> GetOneById(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound("No se encontro el paciente");
            }

            return await _context.Pacientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        //Ingresamos un nuevo paciente
        [HttpPost]
        public async Task<ActionResult> Post(Paciente modelo)
        {
            _context.Add(modelo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //Buscamos en nuestra DB el paciente por el id para editarlo
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Paciente modelo)
        {
            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound("El paciente no se encuentra");
            }

            paciente.Nombre = modelo.Nombre;
            paciente.NumeroTelefono = modelo.NumeroTelefono;

            _context.Update(paciente);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Borrar el paciente de la DB mediante el id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null)
            {
                return NotFound("El paciente especificado no existe");
            }

            _context.Pacientes.Remove(paciente);
            _context.SaveChanges();

            return Ok();
        }
    }

}

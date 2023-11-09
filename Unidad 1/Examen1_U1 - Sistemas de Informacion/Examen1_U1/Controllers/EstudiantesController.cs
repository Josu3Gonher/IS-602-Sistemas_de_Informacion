using Examen1_U1.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examen1_U1.Controllers
{
    [Route("api/estudiantes")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstudiantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Estudiante>>> Get()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Estudiante modelo)
        {
            _context.Add(modelo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("establecer-relacion")]
        public async Task<ActionResult> EstablecerRelacion(int estudianteId, int tutorId)
        {
            var estudiante = _context.Estudiantes.Include(e => e.Tutores).FirstOrDefault(e => e.Id == estudianteId);
            var tutor = _context.Estudiantes.Find(tutorId);

            if (estudiante == null || tutor == null)
            {
                return NotFound("Estudiante o tutor no encontrado");
            }

            estudiante.Tutores.Add(tutor);
            await _context.SaveChangesAsync();

            return Ok("Relación establecida correctamente");
        }

        [HttpPut("{id:int}")] // api/estudiantes/#
        public async Task<IActionResult> Put(int id, Estudiante modelo)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Id == id);
            if (estudiante is null)
            {
                return NotFound("Estudiante no encontrado");
            }

            estudiante.Nombre = modelo.Nombre;
            estudiante.NumeroCuenta = modelo.NumeroCuenta;
            estudiante.Correo = modelo.Correo;
            _context.Update(estudiante);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:int}")] // api/estudiantes/#
        public async Task<ActionResult> Delete(int id)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Id == id);
            if (estudiante is null)
            {
                return NotFound("Estudiante no encontrado");
            }

            _context.Remove(estudiante);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

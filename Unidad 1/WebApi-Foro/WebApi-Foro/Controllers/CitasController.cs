using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Foro.Entities;

namespace WebApi_Foro.Controllers
{
    [Route("api/citas")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly CitasDbContext _citasContext;
        private readonly PacientesDbContext _pacientesContext;

        public CitasController(CitasDbContext citasContext, PacientesDbContext pacientesContext)
        {
            _citasContext = citasContext;
            _pacientesContext = pacientesContext;
        }

        //Traemos todas las citas que tenemos en nuestra DB
        [HttpGet]
        public async Task<ActionResult<List<Cita>>> Get()
        {
            return await _citasContext.Citas.ToListAsync();
        }

        //Buscamos en nuestra DB, una cita por el id especifico
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cita>> GetOneById(int id)
        {
            var cita = await _citasContext.Citas.FindAsync(id);

            if (cita == null)
            {
                return NotFound("No se encontro la cita");
            }

            return await _citasContext.Citas.FirstOrDefaultAsync(x => x.Id == id);
        }

        //Creamos una nueva cita
        [HttpPost]
        public async Task<ActionResult> Post(Cita modelo)
        {
            var paciente = await _pacientesContext.Pacientes.FindAsync(modelo.PacienteId);

            if (paciente == null)
            {
                return NotFound("No existe este paciente en la base de datos");
            }

            // Asigna el PacienteId a la cita.
            modelo.PacienteId = paciente.Id;

            // Agrega la nueva cita al contexto.
            _citasContext.Add(modelo);

            // Guarda los cambios en la base de datos de citas.
            await _citasContext.SaveChangesAsync();

            return Ok();
        }

        //Buscamos en nuestra DB una cita por el id para editarla   
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Cita modelo)
        {
            var cita = await _citasContext.Citas.FindAsync(id);

            if (cita == null)
            {
                return NotFound("La cita no se encuentra");
            }

            // Verifica si el paciente con el PacienteId especificado existe en la base de datos actual.
            var existePaciente = await _pacientesContext.Pacientes.FindAsync(modelo.PacienteId);

            if (existePaciente == null)
            {
                return NotFound("El paciente especificado no existe en la base de datos");
            }

            // Asigna el PacienteId a la cita.
            cita.PacienteId = existePaciente.Id;

            // Actualiza otros campos de la cita si es necesario.
            cita.Fecha = modelo.Fecha;
            cita.NombreDelMedicoAVisitar = modelo.NombreDelMedicoAVisitar;

            // Actualiza la cita en el contexto y guarda los cambios en la base de datos actual.
            _citasContext.Update(cita);
            await _citasContext.SaveChangesAsync();

            return Ok();
        }

        //Borrar la cita de la DB mediante el id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cita = await _citasContext.Citas.FindAsync(id);

            if (cita == null)
            {
                return NotFound("La cita especificada no existe");
            }

            // Elimina la cita de la base de datos actual.
            _citasContext.Citas.Remove(cita);
            await _citasContext.SaveChangesAsync();

            return Ok();

        }
    }
}

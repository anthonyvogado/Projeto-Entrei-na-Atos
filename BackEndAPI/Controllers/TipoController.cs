using BackEndAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoController : ControllerBase
    {
        private readonly Contexto _contexto;

        public TipoController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo>>> GetTipos()
        {
            var tipos = await _contexto
            .Tipos
            .AsNoTracking()
            .ToListAsync();
            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo>> GetTipo(int id)
        {
            var tipo = await _contexto
            .Tipos
            .FindAsync(id);

            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(tipo);
        }

        [HttpPost]
        public async Task<ActionResult<Tipo>> PostTipo(Tipo tipo)
        {
            _contexto.Tipos.Add(tipo);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipo), new { id = tipo.TipoId }, tipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipo(int id, Tipo tipo)
        {
            var existeTipo = await _contexto.Tipos
                .FirstOrDefaultAsync(t => t.TipoId == id);

            if (existeTipo == null)
            {
                return NotFound();
            }

            existeTipo.Nome = tipo.Nome;


            try
            {
                _contexto.Entry(existeTipo).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipo(int id)
        {
            var tipo = await _contexto.Tipos.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }

            _contexto.Tipos.Remove(tipo);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoExiste(int id)
        {
            return _contexto.Tipos.Any(t => t.TipoId == id);
        }
    }
}
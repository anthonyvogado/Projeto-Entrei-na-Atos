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
    public class ClasseController : ControllerBase
    {
        private readonly Contexto _contexto;

        public ClasseController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classe>>> GetClasses()
        {
            var classes = await _contexto
            .Classes
            .AsNoTracking()
            .ToListAsync();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Classe>> GetClasse(int id)
        {
            var classe = await _contexto
            .Classes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClasseId == id);

            if (classe == null)
            {
                return NotFound();
            }

            return Ok(classe);
        }

        [HttpPost]
        public async Task<ActionResult<Classe>> PostClasse(Classe classe)
        {
            _contexto.Classes.Add(classe);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClasse), new { id = classe.ClasseId }, classe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClasse(int id, Classe classe)
        {
            var existeClasse = await _contexto.Classes
               .FirstOrDefaultAsync(c => c.ClasseId == id);

            if (existeClasse == null)
            {
                return NotFound();
            }

            existeClasse.Nome = classe.Nome;

            try
            {
                _contexto.Entry(existeClasse).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClasseExiste(id))
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
        public async Task<IActionResult> DeleteClasse(int id)
        {
            var classe = await _contexto.Classes.FindAsync(id);
            if (classe == null)
            {
                return NotFound();
            }

            _contexto.Classes.Remove(classe);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool ClasseExiste(int id)
        {
            return _contexto.Classes.Any(c => c.ClasseId == id);
        }
    }
}
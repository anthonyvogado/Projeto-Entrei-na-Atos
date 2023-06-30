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
    public class ContraIndicadoTagController : ControllerBase
    {
        private readonly Contexto _contexto;

        public ContraIndicadoTagController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContraIndicadoTag>>> GetContraIndicadoTags()
        {
            var contraIndicadoTags = await _contexto.ContraIndicadoTags.ToListAsync();
            return Ok(contraIndicadoTags);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContraIndicadoTag>> GetContraIndicadoTag(int id)
        {
            var contraIndicadoTag = await _contexto.ContraIndicadoTags.FindAsync(id);

            if (contraIndicadoTag == null)
            {
                return NotFound();
            }

            return Ok(contraIndicadoTag);
        }

        [HttpPost]
        public async Task<ActionResult<ContraIndicadoTag>> PostContraIndicadoTag(ContraIndicadoTag contraIndicadoTag)
        {
            _contexto.ContraIndicadoTags.Add(contraIndicadoTag);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContraIndicadoTag), new { id = contraIndicadoTag.ContraIndicadoTagId }, contraIndicadoTag);
        }

        [HttpPut]
        public async Task<IActionResult> PutContraIndicadoTag(ContraIndicadoTag contraIndicadoTag)
        {
            var existeContraIndicadoTag = await _contexto.ContraIndicadoTags
                .FirstOrDefaultAsync(cit => cit.ContraIndicadoTagId == contraIndicadoTag.ContraIndicadoTagId);

            if (existeContraIndicadoTag == null)
            {
                return NotFound();
            }

            existeContraIndicadoTag.Nome = contraIndicadoTag.Nome;

            try
            {
                _contexto.Entry(existeContraIndicadoTag).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContraIndicadoTagExiste(contraIndicadoTag.ContraIndicadoTagId))
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
        public async Task<IActionResult> DeleteContraIndicadoTag(int id)
        {
            var contraIndicadoTag = await _contexto.ContraIndicadoTags.FindAsync(id);
            if (contraIndicadoTag == null)
            {
                return NotFound();
            }

            _contexto.ContraIndicadoTags.Remove(contraIndicadoTag);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool ContraIndicadoTagExiste(int id)
        {
            return _contexto.ContraIndicadoTags.Any(c => c.ContraIndicadoTagId == id);
        }
    }
}
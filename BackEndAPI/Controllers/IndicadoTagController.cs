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
    public class IndicadoTagController : ControllerBase
    {
        private readonly Contexto _contexto;

        public IndicadoTagController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndicadoTag>>> GetIndicadoTags()
        {
            var indicadoTags = await _contexto.IndicadoTags.ToListAsync();
            return Ok(indicadoTags);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IndicadoTag>> GetIndicadoTag(int id)
        {
            var indicadoTag = await _contexto.IndicadoTags.FindAsync(id);

            if (indicadoTag == null)
            {
                return NotFound();
            }

            return Ok(indicadoTag);
        }

        [HttpPost]
        public async Task<ActionResult<IndicadoTag>> PostIndicadoTag(IndicadoTag indicadoTag)
        {
            _contexto.IndicadoTags.Add(indicadoTag);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIndicadoTag), new { id = indicadoTag.IndicadoTagId }, indicadoTag);
        }

        [HttpPut]
        public async Task<IActionResult> PutIndicadoTag(IndicadoTag indicadoTag)
        {
            var existeIndicadoTag = await _contexto.IndicadoTags
                .FirstOrDefaultAsync(it => it.IndicadoTagId == indicadoTag.IndicadoTagId);

            if (existeIndicadoTag == null)
            {
                return NotFound();
            }

            existeIndicadoTag.Nome = indicadoTag.Nome;

            try
            {
                _contexto.Entry(existeIndicadoTag).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndicadoTagExiste(indicadoTag.IndicadoTagId))
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
        public async Task<IActionResult> DeleteIndicadoTag(int id)
        {
            var indicadoTag = await _contexto.IndicadoTags.FindAsync(id);
            if (indicadoTag == null)
            {
                return NotFound();
            }

            _contexto.IndicadoTags.Remove(indicadoTag);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool IndicadoTagExiste(int id)
        {
            return _contexto.IndicadoTags.Any(i => i.IndicadoTagId == id);
        }
    }
}
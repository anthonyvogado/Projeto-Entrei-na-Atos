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
    public class MedicamentoController : ControllerBase
    {
        private readonly Contexto _contexto;

        public MedicamentoController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicamento>>> GetMedicamentos()
        {
            var medicamentos = await _contexto
                .Medicamentos
                .Include(m => m.Classe)
                .Include(m => m.Tipo)
                .Include(m => m.IndicadoTags)
                .Include(m => m.ContraIndicadoTags)
                .AsNoTracking()
                .ToListAsync();

            return Ok(medicamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamento>> GetMedicamento(int id)
        {
            var medicamento = await _contexto
                .Medicamentos
                .Include(m => m.Classe)
                .Include(m => m.Tipo)
                .Include(m => m.IndicadoTags)
                .Include(m => m.ContraIndicadoTags)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MedicamentoId == id);

            if (medicamento == null)
            {
                return NotFound();
            }

            return Ok(medicamento);
        }

        [HttpPost]
        public async Task<ActionResult<Medicamento>> PostMedicamento(Medicamento medicamento)
        {

            var existeClasse = await _contexto
            .Classes
            .FirstOrDefaultAsync(c => c.ClasseId == medicamento.ClasseId);
            medicamento.Classe = existeClasse;

            var existeTipo = await _contexto
            .Tipos
            .FirstOrDefaultAsync(t => t.TipoId == medicamento.TipoId);
            medicamento.Tipo = existeTipo;

            int idConsulta = 0;
            for (int i = 0; i < medicamento.IndicadoTagIds.Length; i++)
            {
                idConsulta = medicamento.IndicadoTagIds[i];

                var existeIndicadoTag = await _contexto
                .IndicadoTags
                .FirstOrDefaultAsync(it => it.IndicadoTagId == idConsulta);
                medicamento.IndicadoTags.Add(existeIndicadoTag);
            }

            for (int i = 0; i < medicamento.ContraIndicadoTagIds.Length; i++)
            {
                idConsulta = medicamento.ContraIndicadoTagIds[i];

                var existeContraIndicadoTag = await _contexto
                .ContraIndicadoTags
                .FirstOrDefaultAsync(cit => cit.ContraIndicadoTagId == idConsulta);
                medicamento.ContraIndicadoTags.Add(existeContraIndicadoTag);
            }

            _contexto.Medicamentos.Add(medicamento);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicamento), new { id = medicamento.MedicamentoId }, medicamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamento(
            [FromRoute] int id,
            [FromBody] Medicamento medicamento)
        {

            var existeMedicamento = await _contexto.Medicamentos
                .Include(m => m.Classe)
                .Include(m => m.Tipo)
                .Include(m => m.IndicadoTags)
                .Include(m => m.ContraIndicadoTags)
                .FirstOrDefaultAsync(m => m.MedicamentoId == id);

            if (existeMedicamento == null)
            {
                return NotFound();
            }

            existeMedicamento.Nome = medicamento.Nome;
            existeMedicamento.Posologia = medicamento.Posologia;

            var existeClasse = await _contexto
            .Classes
            .FirstOrDefaultAsync(c => c.ClasseId == medicamento.ClasseId);
            existeMedicamento.Classe = existeClasse;

            var existeTipo = await _contexto
            .Tipos
            .FirstOrDefaultAsync(t => t.TipoId == medicamento.TipoId);
            existeMedicamento.Tipo = existeTipo;

            existeMedicamento.Bula = medicamento.Bula;

            // Atualizar as coleções IndicadoTags e ContraIndicadoTags
            /*var existeIndicadoTags = await _contexto.IndicadoTags
                .FirstOrDefaultAsync(eit => eit.IndicadoTagId == medicamento.IndicadoTags);

            var existeContraIndicadoTags = await _contexto.ContraIndicadoTags
                .FirstOrDefaultAsync(ecit => ecit.ContraIndicadoTagId == id);*/


            existeMedicamento.IndicadoTags.Clear();
            existeMedicamento.ContraIndicadoTags.Clear();


            int idConsulta = 0;
            for (int i = 0; i < medicamento.IndicadoTagIds.Length; i++)
            {
                idConsulta = medicamento.IndicadoTagIds[i];

                var existeIndicadoTag = await _contexto
                .IndicadoTags
                .FirstOrDefaultAsync(it => it.IndicadoTagId == idConsulta);
                existeMedicamento.IndicadoTags.Add(existeIndicadoTag);
            }

            for (int i = 0; i < medicamento.ContraIndicadoTagIds.Length; i++)
            {
                idConsulta = medicamento.ContraIndicadoTagIds[i];

                var existeContraIndicadoTag = await _contexto
                .ContraIndicadoTags
                .FirstOrDefaultAsync(cit => cit.ContraIndicadoTagId == idConsulta);
                existeMedicamento.ContraIndicadoTags.Add(existeContraIndicadoTag);
            }

            /* existeMedicamento.IndicadoTags.Clear();
             existeMedicamento.ContraIndicadoTags.Clear();
             existeMedicamento.IndicadoTags = medicamento.IndicadoTags.ToList();
             existeMedicamento.ContraIndicadoTags = medicamento.ContraIndicadoTags.ToList();*/

            try
            {
                _contexto.Entry(existeMedicamento).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicamentoExiste(id))
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
        public async Task<IActionResult> DeleteMedicamento(int id)
        {
            var medicamento = await _contexto.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            _contexto.Medicamentos.Remove(medicamento);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicamentoExiste(int id)
        {
            return _contexto.Medicamentos.Any(m => m.MedicamentoId == id);
        }
    }
}
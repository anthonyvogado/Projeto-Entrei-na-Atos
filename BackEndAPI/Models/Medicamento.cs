using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEndAPI.Models
{
    public class Medicamento
    {
        [Key]
        public int MedicamentoId { get; set; }

        public string Nome { get; set; }
        public string Posologia { get; set; }

        [ForeignKey("Classe")]
        public int ClasseId { get; set; }
        [JsonIgnore]
        public Classe? Classe { get; set; }

        [ForeignKey("Tipo")]
        public int TipoId { get; set; }
        [JsonIgnore]
        public Tipo? Tipo { get; set; }

        [NotMapped]
        public int[] IndicadoTagIds { get; set; }

        [JsonIgnore]
        public ICollection<IndicadoTag>? IndicadoTags { get; set; } = new List<IndicadoTag>();

        [NotMapped]
        public int[] ContraIndicadoTagIds { get; set; }

        [JsonIgnore]
        public ICollection<ContraIndicadoTag>? ContraIndicadoTags { get; set; } = new List<ContraIndicadoTag>();

        public string Bula { get; set; }

        public string PrincipioAtivo { get; set; }
    }
}
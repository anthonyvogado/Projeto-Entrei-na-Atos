using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEndAPI.Models
{

    public class ContraIndicadoTag
    {
        [Key]
        [JsonIgnore]
        public int ContraIndicadoTagId { get; set; }

        public string? Nome { get; set; }

        [JsonIgnore]
        public ICollection<Medicamento>? Medicamentos { get; set; }
    }

}
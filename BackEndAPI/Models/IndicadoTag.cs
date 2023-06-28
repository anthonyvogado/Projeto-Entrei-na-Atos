using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEndAPI.Models
{

    public class IndicadoTag
    {
        [Key]
        [JsonIgnore]
        public int IndicadoTagId { get; set; }

        public string? Nome { get; set; }

        [JsonIgnore]
        public ICollection<Medicamento>? Medicamentos { get; set; }
    }

}
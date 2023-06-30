using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEndAPI.Models
{

    public class Classe
    {
        [Key]
        public int ClasseId { get; set; }

        public string? Nome { get; set; }

        [JsonIgnore]
        public ICollection<Medicamento>? Medicamentos { get; set; }
    }

}
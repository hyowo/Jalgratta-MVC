using System.ComponentModel.DataAnnotations;

namespace Jalgratta_Eksam.Models
{
    public class Eksam
    {
        public int ID { get; set; }

        [StringLength(64)]
        [Required]
        public string? Eesnimi { get; set; }

        [StringLength(64)]
        [Required]
        public string? Perekonnanimi { get; set; }

        [Range(-1, 10)]
        public int Teooria { get; set; } = -1;
        public int Slaalom { get; set; } = -1;
        public int Ring { get; set; } = -1;
        public int Tanav { get; set; } = -1;
        public int Luba { get; set; } = 0;
    }
}

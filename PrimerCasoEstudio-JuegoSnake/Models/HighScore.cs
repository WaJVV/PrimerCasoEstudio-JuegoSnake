using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimerCasoEstudio_JuegoSnake.Models
{
    public class HighScore
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("User")]
        public String user { get; set; }
        public String time { get; set; }
        public String score { get; set; }
    }
}

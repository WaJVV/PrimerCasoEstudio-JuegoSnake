using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimerCasoEstudio_JuegoSnake.Models
{
    public class User
    {
        [Key]

        public String user { get; set; }
        public String password { get; set; }

        
        public Byte[] photo { get; set; }



    }
}

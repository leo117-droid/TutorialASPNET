using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre requerido")]
        [MaxLength(60, ErrorMessage = "Maximo de caracteres debe ser 60")]
        public string Nombre { get; set; }

        

        [Required(ErrorMessage = "Descripcion requerida")]
        [MaxLength(100, ErrorMessage = "Maximo de caracteres debe ser 100")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado requerida")]
        public bool Estado { get; set; }
        
    }
}

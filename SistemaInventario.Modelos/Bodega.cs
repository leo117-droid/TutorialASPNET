using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Bodega
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Id es Requerido")]
        [MaxLength(60, ErrorMessage = "Excede la cantidad de caracteres, maximo 60")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Nombre es Requerido")]
        [MaxLength(100, ErrorMessage = "Excede la cantidad de caracteres, maximo 100")]
        public string Descripcion { get; set;}

        [Required(ErrorMessage = "Estado es Requerido")]
        public bool Estado { get; set;}
    }
}

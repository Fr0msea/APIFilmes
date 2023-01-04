using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; internal set; }

        [Required(ErrorMessage ="o titulo é obrigatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "o genero é obrigatorio")]
        [MaxLength(50, ErrorMessage = "o tamanho do genero é até 50 caracteres")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "a duracao é obrigatorio")]
        [Range(70,600, ErrorMessage = "a duracao é entre 70 e 600 min")]
        public int Duracao { get; set; }

    }
}

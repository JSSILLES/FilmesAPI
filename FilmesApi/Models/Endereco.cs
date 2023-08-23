using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Logradouro { get; set; }
        public int Numero { get; set; }

        //possuir um e apenas um endereço (1:1)
        public virtual Cinema Cinema { get; set; }



    }
}

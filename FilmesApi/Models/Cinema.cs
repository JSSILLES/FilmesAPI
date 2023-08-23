using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo de nome é obrigatório.")]
        public string Nome { get; set; }


        //para que um cinema seja criado, o endereço deve
        //obrigatoriamente já existir.
        public int EnderecoId { get; set; }

        //possuir um e apenas um endereço (1:1)
        public virtual Endereco Endereco { get; set; }


    }
}

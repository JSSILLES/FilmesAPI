using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos
{
    public class CreateEnderecoDTO
    {
        public string Logradouro { get; set; }
        public int Numero { get; set; }
    }
}

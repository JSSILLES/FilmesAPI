using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        //Injeção de dependência
        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        ///  Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDTO">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDTO filmeDTO)
        {
            Filme filme = _mapper.Map<Filme>(filmeDTO);
            _context.Filme.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarFilmeId), new { id = filme.IdFilme }, filme);
        }

        [HttpGet]
        //usando IEnumerable da forma mais genérica possível
        public IEnumerable<ReadFilmeDTO> ListarFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadFilmeDTO>>(_context.Filme.Skip(skip).Take(take));
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmeId(int id)
        {
            var filme = _context.Filme.FirstOrDefault(filme => filme.IdFilme == id);
            if (filme == null)
                return NotFound();

            var filmeDTO = _mapper.Map<ReadFilmeDTO>(filme);
            return Ok(filmeDTO);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarFilmeId(int id, [FromBody] UpdateFilmeDTO filmeDTO)
        {
            var filme = _context.Filme.FirstOrDefault(filme => filme.IdFilme == id);
            if (filme == null)
                return NotFound();

            _mapper.Map(filmeDTO, filme);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDTO> patch)
        {
            var filme = _context.Filme.FirstOrDefault(filme => filme.IdFilme == id);
            if (filme == null)
                return NotFound();

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDTO>(filme);

            /* A mudança que estamos tentando aplicar - patch */
            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if (!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarFilme(int id)
        {
            var filme = _context.Filme.FirstOrDefault(filme => filme.IdFilme == id);
            if (filme == null)
                return NotFound();

            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.DTO;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController:ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(IMapper mapper, FilmeContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            try
            {
                Filme filme = _mapper.Map<Filme>(filmeDto);
                _context.Filmes.Add(filme);
                _context.SaveChanges();
                return CreatedAtAction(nameof(RecuperaFilmeId), new { id = filme.Id }, filme);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }

        }

        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes([FromQuery]int skip =0, [FromQuery] int take = 10)
        {
            return _context.Filmes.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]

        public IActionResult RecuperaFilmeId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(c => c.Id == id);
            if (filme == null) return NotFound();
            return Ok(filme);

        }

        [HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id,
            [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

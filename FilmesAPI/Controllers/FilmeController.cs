using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = new Filme
            {
                Titulo = filmeDto.Titulo,
                Genero = filmeDto.Genero,
                Diretor = filmeDto.Diretor,
                Duracao = filmeDto.Duracao
            };

            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = filme.Id }, filme);
        }

        [HttpGet]
        public IActionResult RecuperaFilmes()
        {
            return Ok(_context.Filmes);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                ReadFilmeDto filmeDto = new ReadFilmeDto
                {
                    Titulo = filme.Titulo,
                    Diretor = filme.Diretor,
                    Duracao = filme.Duracao,
                    Id = filme.Id,
                    Genero = filme.Genero,
                    HoraDaConsulta = DateTime.Now
                };
                return Ok(filmeDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            filme.Titulo = filmeDto.Titulo;
            filme.Genero = filmeDto.Genero;
            filme.Duracao = filmeDto.Duracao;
            filme.Diretor = filmeDto.Diretor;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

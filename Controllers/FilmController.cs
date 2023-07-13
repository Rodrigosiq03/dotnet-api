using DotNetAPI.Data;
using DotNetAPI.Data.DTOs;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
    private FilmContext _context;
    public FilmController(FilmContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddFilm([FromBody] CreateFilmDto filmDto)
    {
        Film film = Film.ToModel(new Film(), filmDto);

        _context.Films.Add(film);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetFilmById), new { id = film.Id }, film);
        
    }

    [HttpGet]
    public IEnumerable<ReadFilmDto> GetFilms([FromQuery(Name = "skip")] int skip = 0, [FromQuery(Name = "take")] int take = 20) 
    {   
        return _context.Films
            .Skip(skip)
            .Take(take)
            .Select(film => Film.ToDto(new ReadFilmDto(), film));
    }

    [HttpGet("get-by-id")]
    public IActionResult GetFilmById([FromQuery(Name = "id")] int id)
    {
        Film filmGot = _context.Films.FirstOrDefault(film => film.Id == id)!;
        if (filmGot == null) return NotFound();
        var filmDto = Film.ToDto(new ReadFilmDto(), filmGot);

        return Ok(filmDto);
    }

    [HttpPut]
    public IActionResult UpdateFilm([FromQuery(Name = "id")] int id, [FromBody] UpdateFilmDto filmDto)
    {
        Film filmGot = _context.Films.FirstOrDefault(film => film.Id == id)!;
        if (filmGot == null) return NotFound();
        Film film = Film.ToModel(filmGot, filmDto);
        _context.Films.Update(film);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch]
    public IActionResult UpdateFilmPatch([FromQuery(Name = "id")] int id, 
        [FromBody] JsonPatchDocument<UpdateFilmDto> patchDocument)
    {
        Film filmGot = _context.Films.FirstOrDefault(film => film.Id == id)!;
        if (filmGot == null) return NotFound();

        UpdateFilmDto filmToUpdate = Film.ToDto(new UpdateFilmDto(), filmGot);
        patchDocument.ApplyTo(filmToUpdate, ModelState);

        if (!TryValidateModel(filmToUpdate)) return ValidationProblem(ModelState);

        Film.ToModel(filmGot, filmToUpdate);
        _context.Films.Update(filmGot);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete]

    public IActionResult DeleteFilm([FromQuery(Name = "id")] int id)
    {
        Film filmGot = _context.Films.FirstOrDefault(film => film.Id == id)!;
        if (filmGot == null) return NotFound();

        _context.Remove(filmGot);
        _context.SaveChanges();
        return NoContent();
    }
}
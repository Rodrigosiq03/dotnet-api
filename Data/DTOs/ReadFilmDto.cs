using System.ComponentModel.DataAnnotations;

namespace DotNetAPI.Data.DTOs;

public class ReadFilmDto
{
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public int Duration { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.Now;
}
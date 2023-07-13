using System.ComponentModel.DataAnnotations;

namespace DotNetAPI.Data.DTOs;

public class UpdateFilmDto 
{
    [Required(ErrorMessage = "The title of the film is required")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "The genre of the film is required")]
    [StringLength(50, ErrorMessage = "Only 50 characters are available")]
    public string? Genre { get; set; }
    [Required(ErrorMessage = "The duration of the film is required")]
    [Range(70, 240, ErrorMessage = "The duration must be between 70 and 240 minutes")]
    public int Duration { get; set; }
}
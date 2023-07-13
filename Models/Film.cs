using System.ComponentModel.DataAnnotations;
using DotNetAPI.Data.DTOs;

namespace DotNetAPI.Models;

public class Film
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "The title of the film is required")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "The genre of the film is required")]
    [MaxLength(50, ErrorMessage = "Only 50 characters are available")]
    public string? Genre { get; set; }
    [Required(ErrorMessage = "The duration of the film is required")]
    [Range(70, 240, ErrorMessage = "The duration must be between 70 and 240 minutes")]
    public int Duration { get; set; }

    // public static Film ToFilm(Film film, Dto filmDto)
    // {
    //     film.Title = filmDto.Title;
    //     film.Genre = filmDto.Genre;
    //     film.Duration = filmDto.Duration;
    //     return film;
    // }

    public static TModel ToModel<TModel, TDto>(TModel model, TDto dto) 
        where TModel : class
        where TDto : class
    {
        var properties = typeof(TDto).GetProperties();
        foreach (var property in properties)
        {
            var modelProperty = typeof(TModel).GetProperty(property.Name);
            modelProperty?.SetValue(model, property.GetValue(dto));
        }
        return model;
    }

    public static TDto ToDto<TDto, TModel>(TDto dto, TModel model) 
        where TDto : class
        where TModel : class
    {
        var properties = typeof(TDto).GetProperties();
        foreach (var property in properties)
        {
            var modelProperty = typeof(TModel).GetProperty(property.Name);
            property.SetValue(dto, modelProperty?.GetValue(model));
        }
        return dto;
    }
}
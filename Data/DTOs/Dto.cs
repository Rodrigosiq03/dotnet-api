namespace DotNetAPI.Data.DTOs;

public class TDto<T>
{
    public T? Id { get; set; }
    public T? Title { get; set; }
    public T? Genre { get; set; }
    public T? Duration { get; set; }
    public DateTime? TimeStamp { get; set; } = DateTime.Now;
}
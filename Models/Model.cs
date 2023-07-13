namespace DotNetAPI.Models;

public class TModel<T>
{
    public T? Id { get; set; }
    public T? Title { get; set; }
    public T? Genre { get; set; }
    public T? Duration { get; set; }
    public DateTime? TimeStamp { get; set; } = DateTime.Now;
}
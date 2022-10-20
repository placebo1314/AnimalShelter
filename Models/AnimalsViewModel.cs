namespace TestProject02.Models;

public class AnimalsViewModel
{
    public bool EnableNext { get; set; }
    public bool EnablePrevious { get; set; }
    public int Page { get; set; }
    public List<Animal> Animals { get; set; }
    public string? Species { get; set; }
    public string? SortBy { get; set; }
    public string? Order { get; set; }
}

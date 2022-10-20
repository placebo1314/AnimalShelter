using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TestProject02.Models;

public class Animal
{
    public int Id { get; set; }
    public string Name{ get; set; }
    public string? Species { get; set; }
    public string? Description { get; set; }
    public int? Age { get; set; }
    public string? Color { get; set; }
    public string? Type { get; set; }
    public string? Image { get; set; }
    public string? BgImage { get; set; }
    public string? Inclusion { get; set; }
}
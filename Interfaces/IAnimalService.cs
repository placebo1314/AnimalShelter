using TestProject02.Models;

namespace AnimalShelter.Interfaces;

public interface IAnimalService
{
    public string AddAnimal(Animal animal, IFormFile image, IFormFile bgImage, string path, string folder);
    public IEnumerable<Animal> GetAllAnimals(string? filter);
    public string DeleteAnimal(int id, string path);
    public string EditAnimal(Animal animal, IFormFile image, IFormFile bgImage, string path, string folder);
    public Animal GetAnimal(int id);

    
}
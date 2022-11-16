using TestProject02.Models;

namespace AnimalShelter.Interfaces;

public interface IAnimalsDao
{
    Animal Get(int id);
    IEnumerable<Animal> GetAll(string filter);
    void Add(Animal item);
    void Delete(int id);
    void Edit(Animal animal);
}

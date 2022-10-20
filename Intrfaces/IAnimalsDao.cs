using TestProject02.Models;

namespace TestProject02.Intrfaces;

public interface IAnimalsDao
{
    Animal Get(int id);
    IEnumerable<Animal> GetAll(string filter);
    void Add(Animal item);
    void Delete(int id);
    void Edit(Animal animal);
}

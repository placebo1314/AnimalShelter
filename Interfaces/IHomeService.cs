using TestProject02.Models;

namespace AnimalShelter.Interfaces;

public interface IHomeService
{
    AnimalsViewModel GetDisplayAnimalsData(string species, string sortBy, int page, string order, bool change);
}

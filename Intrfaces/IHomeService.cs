using TestProject02.Models;

namespace TestProject02.Intrfaces;

public interface IHomeService
{
    AnimalsViewModel GetDisplayAnimalsData(string species, string sortBy, int pageSize, int page, string order, bool change);

}

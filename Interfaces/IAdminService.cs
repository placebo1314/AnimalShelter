using TestProject02.Models;

namespace AnimalShelter.Interfaces;

public interface IAdminService
{
    public void RemoveAdmin(int id);
    public void AddAdmin(int id);
    public IEnumerable<User> GetAllUsers();
}
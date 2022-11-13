using TestProject02.Models;

namespace TestProject02.Intrfaces;

public interface IAccountDao
{
    List<User> GetAll();
    void Add(RegisterModel item);
    User Login(LoginModel login);
    void RemoveAdmin(int id);
    void AddAdmin(int id);
}

using TestProject02.Models;

namespace AnimalShelter.Interfaces
{
    public interface IAccountService
    {
        void AddUser(RegisterModel register);
        public User Login(LoginModel login);
        public void Logout(ISession session);
        public void RemoveAdmin(int id);
        public void AddAdmin(int id);
        public IEnumerable<User> GetAllUsers();

    }
}
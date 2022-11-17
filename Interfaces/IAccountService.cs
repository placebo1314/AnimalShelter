using TestProject02.Models;

namespace AnimalShelter.Interfaces
{
    public interface IAccountService
    {
        void AddUser(RegisterModel register);
        public User Login(LoginModel login);
        public void Logout(ISession session);
    }
}
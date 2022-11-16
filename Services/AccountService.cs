using AnimalShelter.Interfaces;
using TestProject02.Models;

namespace AnimalShelter.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDao _userDao;
        public AccountService(IAccountDao userDao)
        {
            _userDao = userDao;
        }

        public void AddUser(RegisterModel register)
        {
            _userDao.Add(register);
        }
        public User Login(LoginModel login)
        {
           return _userDao.Login(login);
        }
        public void Logout(ISession session)
        {
            session.Clear();
        }

        public void RemoveAdmin(int id)
        {
            _userDao.RemoveAdmin(id);
        }
        public void AddAdmin(int id)
        {
            _userDao.AddAdmin(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userDao.GetAll();
        }
        

        //public void AddAdmin(RegisterAdminModel register)
        //{
        //    _userDao.AddAdmin(register);
        //}

    }
}

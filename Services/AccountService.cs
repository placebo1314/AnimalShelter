using TestProject02.Daos;
using TestProject02.Intrfaces;
using TestProject02.Models;

namespace TestProject02.Services
{
    public class AccountService
    {
        private readonly IAccountDao _userDao;
        //private static AccountService instance;

        public AccountService(IAccountDao userDao)
        {
            _userDao = userDao;
        }
        //public static AccountService GetInstance(string connectionString)
        //{
        //    if (instance == null)
        //    {
        //        instance = new AccountService(RegisterDao.GetInstance(connectionString));
        //    }

        //    return instance;
        //}

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

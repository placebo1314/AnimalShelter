using AnimalShelter.Daos;
using NUnit.Framework;
using TestProject02.Models;

namespace UnitTests
{
    public class Tests
    {
        private RegisterDao? registerDao;

        [SetUp]
        public void Setup()
        {
            registerDao = RegisterDao.GetInstance("Server=(LocalDb)\\MSSQLLocalDB;Database=TestProject02;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        [Test]
        public void AccountDAO_GetAll_ReturnsList_Test()
        {
            var result = registerDao.GetAll();

            Assert.That(result, Is.TypeOf<List<User>>());
        }
        [Test]
        public void AccountDAO_AddUser_Success_Test()
        {
            RegisterModel item = new RegisterModel()
            {
                Username = "aaaaba",
                Password = "aaaaba",
                Email = "aaaaba@aaaaba.pu"
            };
            registerDao.Add(item);
            var isContain = false;
            foreach (var user in registerDao.GetAll())
                if (user.Name == item.Username && user.Email == item.Email)
                    isContain = true;

            Assert.That(isContain, Is.EqualTo(true));

            registerDao.DeleteUser(item.Username);
        }
        [Test]
        public void AccountDAO_Login_ReturnsUser_Test()
        {
            LoginModel login = new LoginModel()
            {
                Email = "aa@aa",
                Password = "aa"
            };
            var result = registerDao.Login(login);

            Assert.IsTrue(result.Name == "aa");
        }
        [Test]
        public void AccountDAO_AddAdmin_Test()
        {
            //The second user is an default user
            registerDao.AddAdmin(2);
            User user = registerDao.GetAll().Where(i => i.Id == 2).FirstOrDefault();

            Assert.IsTrue(user.Admin == "Y");

            registerDao.RemoveAdmin(2);
        }
        [Test]
        public void AccountDAO_RemoveAdminAdmin_Test()
        {
            //The first user is an default admin
            registerDao.RemoveAdmin(1);
            User user = registerDao.GetAll().Where(i => i.Id == 1).FirstOrDefault();

            Assert.IsTrue(user.Admin == "N");

            registerDao.AddAdmin(1);
        }
    }
}
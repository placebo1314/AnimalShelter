using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
//using Codecool.CodecoolShop.Daos.Implementations;
using TestProject02.Models;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using TestProject02.Intrfaces;

namespace TestProject02.Daos
{
    public class RegisterDao : IAccountDao
    {
        private readonly string _connectionString;
        private static RegisterDao instance;


        private RegisterDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static RegisterDao GetInstance(string connectionString)
        {
            if (instance == null)
            {
                instance = new RegisterDao(connectionString);
            }

            return instance;
        }

        public IEnumerable<User> GetAll()
        {
            const string cmdText = @"SELECT * FROM users;";
            try
            {
                var results = new List<User>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        var user = new User()
                        {
                            Id = (int)reader["id"],
                            Name = (string)reader["name"],
                            Email = reader["email_address"] != DBNull.Value ? (string)reader["email_address"] : null,
                            Admin = reader["admin"] != DBNull.Value ? (string)reader["admin"] : null,

                        };
                        results.Add(user);
                    }
                    connection.Close();
                }
                return results;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void Add(RegisterModel item)
        {
            const string cmdText = @"INSERT INTO users (name,password,email_address, admin)
                                VALUES (@name, @password, @email_address, 'N')
                                SELECT SCOPE_IDENTITY();";

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(cmdText, connection);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                cmd.Parameters.AddWithValue("@name", item.Username);
                cmd.Parameters.AddWithValue("@password", Utils.PasswordHelper.HashString(item.Password));

                cmd.Parameters.AddWithValue("@email_address", item.Email);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }



        //public void AddAdmin(RegisterAdminModel item)
        //{
        //    if (Utils.PasswordHelper.HashString(item.AdminKey) == "C1C224B03CD9BC7B6A86D77F5DACE40191766C485CD55DC48CAF9AC873335D6F")
        //        {
        //        const string cmdText = @"INSERT INTO adm1ns (name,password)
        //                        VALUES (@name, @password)
        //                        SELECT SCOPE_IDENTITY();";

        //        using (var connection = new SqlConnection(_connectionString))
        //        {
        //            var cmd = new SqlCommand(cmdText, connection);
        //            if (connection.State == ConnectionState.Closed)
        //                connection.Open();

        //            cmd.Parameters.AddWithValue("@name", item.Username);
        //            cmd.Parameters.AddWithValue("@password", Utils.PasswordHelper.HashString(item.Password));


        //            cmd.ExecuteNonQuery();
        //            connection.Close();
        //        }
        //    } else{
               
        //    }
        //}

        public User Login(LoginModel login)
        {
            const string cmdText = @"SELECT * FROM users WHERE email_address = @email_address";
            try
            {
                User user = null;
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@email_address", login.Email);
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return null;

                    while (reader.Read())
                    {
                        if (Equals(Utils.PasswordHelper.HashString(login.Password), (string)reader["password"]))
                        {
                            user = new User()
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Admin = (string)reader["admin"]
                            };
                        }
                    }
                    connection.Close();
                }

                return user;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void RemoveAdmin(int id)
        {
            const string cmdText = @"UPDATE users SET admin='N'
                                WHERE id=@id;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void AddAdmin(int id)
        {
            const string cmdText = @"UPDATE users SET admin='Y'
                                WHERE id=@id;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }






    }
}

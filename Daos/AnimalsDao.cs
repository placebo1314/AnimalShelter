using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using AnimalShelter.Interfaces;
using TestProject02.Models;

namespace AnimalShelter.Daos
{
    public class AnimalsDao : IAnimalsDao
    {
        private readonly string _connectionString;
        private static AnimalsDao? _instance;

        private AnimalsDao(string connectionString)
        {
            _connectionString = connectionString;
        }
        public static AnimalsDao? GetInstance(string connectionString)
        {
            if (_instance == null)
            {
                _instance = new AnimalsDao(connectionString);
            }
            return _instance;
        }

        public Animal Get(int id)
        {
            Animal animal= new Animal();
            const string cmdText = @"SELECT * FROM animals WHERE id=@id;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    
                    if (reader.Read())
                    {
                        animal.Id = (int)reader["id"];
                        animal.Name = (string)reader["name"];
                        animal.Description = reader["description"] != DBNull.Value ? (string)reader["description"] : null;
                        animal.Age = reader["age"] != DBNull.Value ? (int)reader["age"] : null;
                        animal.Sex = reader["sex"] != DBNull.Value ? (string)reader["sex"] : null;
                        animal.Type = reader["type"] != DBNull.Value ? (string)reader["type"] : null;
                        animal.Image = reader["image"] != DBNull.Value ? (string)reader["image"] : null;
                        animal.BgImage = reader["bgimage"] != DBNull.Value ? (string)reader["bgimage"] : null;
                        animal.Species = reader["species"] != DBNull.Value ? (string)reader["species"] : null;
                        animal.Inclusion = reader["inclusion"] != DBNull.Value ? (string)reader["inclusion"] : null;

                    }

                    connection.Close();
                }
                return animal;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

            public  IEnumerable<Animal> GetAll(string filter)
            {
            string cmdText;
            if (filter == null || filter == "Mind")
                cmdText = @"SELECT * FROM animals;";
            else
                cmdText = @"SELECT * FROM animals WHERE species=@species;";
            
            try
            {
                var results = new List<Animal>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    if (filter != null && filter != "Mind")
                        cmd.Parameters.AddWithValue("@species", filter);
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        var animal = new Animal()
                        {
                            Id = (int)reader["id"],
                            Name = (string)reader["name"],
                            Description = reader["description"] != DBNull.Value ? (string)reader["description"] : null,
                            Age = reader["age"] != DBNull.Value ? (int)reader["age"] : null,
                            Sex = reader["sex"] != DBNull.Value ? (string)reader["sex"] : null,
                            Type = reader["type"] != DBNull.Value ? (string)reader["type"] : null,
                            Image = reader["image"] != DBNull.Value ? (string)reader["image"] : null,
                            BgImage = reader["bgimage"] != DBNull.Value ? (string)reader["bgimage"] : null,
                            Species = reader["species"] != DBNull.Value ? (string)reader["species"] : null,
                            Inclusion = reader["inclusion"] != DBNull.Value ? (string)reader["inclusion"] : null,
                        };
                        results.Add(animal);
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
        public void Add(Animal item)
        {
            const string cmdText = @"INSERT INTO animals (name, description, age, sex, type, image, bgimage, species, inclusion)
                                VALUES (@name, @description, @age, @sex, @type, @image, @bgimage, @species, @inclusion)
                                SELECT SCOPE_IDENTITY();";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@description", item.Description != null ? item.Description : DBNull.Value);
                    cmd.Parameters.AddWithValue("@type", item.Type != null ? item.Type : DBNull.Value);
                    cmd.Parameters.AddWithValue("@age", item.Age != null ? item.Age : DBNull.Value);
                    cmd.Parameters.AddWithValue("@sex", item.Sex != null ? item.Sex : DBNull.Value);
                    cmd.Parameters.AddWithValue("@image", item.Image != null ? item.Image : DBNull.Value);
                    cmd.Parameters.AddWithValue("@bgimage", item.BgImage != null ? item.BgImage : DBNull.Value);
                    cmd.Parameters.AddWithValue("@species", item.Species != null ? item.Species : DBNull.Value);
                    cmd.Parameters.AddWithValue("@inclusion", item.Inclusion != null ? item.Inclusion : DBNull.Value);

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void Delete(int id)
        {
            const string cmdText = @"DELETE FROM animals WHERE id=@id;";
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

        public void Edit(Animal animal)
        {
            const string cmdText = @"UPDATE animals SET name=@name, description=@description, age=@age, sex=@sex, type=@type, image=@image, bgimage=@bgimage, species=@species
                                WHERE id=@id;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(cmdText, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    cmd.Parameters.AddWithValue("@id", animal.Id);
                    cmd.Parameters.AddWithValue("@name", animal.Name);
                    cmd.Parameters.AddWithValue("@description", animal.Description != null ? animal.Description : DBNull.Value);
                    cmd.Parameters.AddWithValue("@type", animal.Type != null ? animal.Type : DBNull.Value);
                    cmd.Parameters.AddWithValue("@age", animal.Age != null ? animal.Age : DBNull.Value);
                    cmd.Parameters.AddWithValue("@sex", animal.Sex != null ? animal.Sex : DBNull.Value);
                    cmd.Parameters.AddWithValue("@image", animal.Image != null ? animal.Image : DBNull.Value);
                    cmd.Parameters.AddWithValue("@bgimage", animal.BgImage != null ? animal.BgImage : DBNull.Value);
                    cmd.Parameters.AddWithValue("@species", animal.Species != null ? animal.Species : DBNull.Value);
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

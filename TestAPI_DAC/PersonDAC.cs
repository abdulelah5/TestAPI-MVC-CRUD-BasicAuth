using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPI_DTO;

namespace TestAPI_DAC
{
    public class PersonDAC
    {
        private readonly string ConnString;
        public PersonDAC()
        {
            ConnString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }
        protected SqlConnection GetConnection()
        {
            var con = new SqlConnection(ConnString);
            return con;
        }

        public Person FetchPersonName(int id)
        {
            if (id <= 0)
                return new Person();

            var result = new Person();
            var con = GetConnection();
            try
            {                
                var sql = "SELECT Name FROM Person WHERE ID = @id;";
                var cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@id",id);
                con.Open();
                result.name = Convert.ToString(cmd.ExecuteScalar());

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public Person FetchPersonInfo(int id)
        {
            if (id <= 0)
                return new Person();

            var result = new Person();
            var con = GetConnection();
            try
            {
                var sql = "SELECT * FROM Person WHERE ID = @id;";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    result.id = (int)reader[0];
                    result.name = Convert.ToString(reader[1]);
                    result.phone = Convert.ToString(reader[2]);
                }

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Person> FetchAllNames()
        {
            var result = new List<Person>();
            var con = GetConnection();
            try
            {
                var sql = "SELECT * FROM Person;";
                var cmd = new SqlCommand(sql, con);
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Person person = new Person();
                    person.id = (int)reader[0];
                    person.name = Convert.ToString(reader[1]);
                    person.phone = Convert.ToString(reader[2]);

                    result.Add(person);
                }

                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int InsertPersonInfo(Person person)
        {
            if (person == null)
                return 0;

            var result = 0;
            var con = GetConnection();
            try
            {
                var sql = "INSERT INTO Person(Name,Phone) output INSERTED.ID VALUES(@name,@phone)";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", person.name);
                cmd.Parameters.AddWithValue("@phone", person.phone);
                con.Open();
                result = (int)cmd.ExecuteScalar();

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public bool UpdatePersonInfo(Person person)
        {
            if (person == null)
                return false;

            var result = false;
            var con = GetConnection();
            try
            {
                var sql = "UPDATE Person SET Name = @name, Phone = @phone WHERE ID = @id";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", person.id);
                cmd.Parameters.AddWithValue("@name", person.name);
                cmd.Parameters.AddWithValue("@phone", person.phone);
                con.Open();
                result = cmd.ExecuteNonQuery() > 0 ? true : false;

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public bool DeletePersonInfo(int id)
        {
            if (id == 0)
                return false;

            var result = false;
            var con = GetConnection();
            try
            {
                var sql = "DELETE FROM Person WHERE ID = @id";
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                result = cmd.ExecuteNonQuery() > 0 ? true : false;

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}

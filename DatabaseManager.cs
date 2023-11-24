using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace lab4_KosinovaAiLib
{

    public class DatabaseManager
    {
        private readonly string connectionString;

        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Message GetByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Message>("SELECT * FROM Messages WHERE ID = @ID", new { ID = id });
            }
        }

        public List<Message> GetByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Message>("SELECT * FROM Messages WHERE Name = @Name", new { Name = name }).ToList();
            }
        }

        public void Add(Message message)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Messages (Name, Message) VALUES (@Name, @Message)", message);
            }
        }

        public void Update(int id, string newMessage)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("UPDATE Messages SET Message = @Message WHERE ID = @ID", new { ID = id, Message = newMessage });
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("DELETE FROM Messages WHERE ID = @ID", new { ID = id });
            }
        }
    }

    public class Message
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MessageText { get; set; }
    }
}

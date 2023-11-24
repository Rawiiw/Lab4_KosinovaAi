using System;
using System.Collections.Generic;
using Xunit;

namespace lab4_KosinovaAiLib.Tests
{
    public class DatabaseManagerTests
    {
        private readonly string connectionString = @"Data Source=НЕФОР;Initial Catalog=Demo;Integrated Security=True";

        [Fact]
        public void AddAndGetById_Test()
        {
          
            DatabaseManager dbManager = new DatabaseManager(connectionString);
            Message testMessage = new Message { ID = 1, Name = "TestUser", MessageText = "TestMessage" };

            
            dbManager.Add(testMessage);
            Message retrievedMessage = dbManager.GetByID(1);

      
            Assert.NotNull(retrievedMessage);
            Assert.Equals(testMessage.ID, retrievedMessage.ID);
            Assert.Equals(testMessage.Name, retrievedMessage.Name);
            Assert.Equals(testMessage.MessageText, retrievedMessage.MessageText);
        }

        [Fact]
        public void GetByName_Test()
        {
          
            DatabaseManager dbManager = new DatabaseManager(connectionString);
            string testName = "TestUser";
            Message testMessage1 = new Message { ID = 2, Name = testName, MessageText = "TestMessage1" };
            Message testMessage2 = new Message { ID = 3, Name = testName, MessageText = "TestMessage2" };

            // Act
            dbManager.Add(testMessage1);
            dbManager.Add(testMessage2);
            List<Message> messages = dbManager.GetByName(testName);

            Assert.NotNull(messages);
            Assert.Equals(2, messages.Count);

            foreach (var message in messages)
            {
                Assert.Equals(testName, message.Name);
            }
        }

        [Fact]
        public void Update_Test()
        {
            
            DatabaseManager dbManager = new DatabaseManager(connectionString);
            int messageId = 4;
            Message testMessage = new Message { ID = messageId, Name = "TestUser", MessageText = "OriginalMessage" };
            dbManager.Add(testMessage);

     
            string newMessageText = "UpdatedMessage";
            dbManager.Update(messageId, newMessageText);
            Message updatedMessage = dbManager.GetByID(messageId);

            
            Assert.NotNull(updatedMessage);
            Assert.Equals(messageId, updatedMessage.ID);
            Assert.Equals(testMessage.Name, updatedMessage.Name);
            Assert.Equals(newMessageText, updatedMessage.MessageText);
        }

        [Fact]
        public void Delete_Test()
        {
       
            DatabaseManager dbManager = new DatabaseManager(connectionString);
            int messageId = 5;
            Message testMessage = new Message { ID = messageId, Name = "TestUser", MessageText = "TestMessage" };
            dbManager.Add(testMessage);

       
            dbManager.Delete(messageId);
            Message deletedMessage = dbManager.GetByID(messageId);

        
            Assert.Null(deletedMessage);
        }
    }
}

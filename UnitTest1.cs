using lab4_KosinovaAiLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace lab4_KosinovaAiLibTests
{
    [TestClass]
        public class DatabaseManagerTests
        {
            private const string ConnectionString = @"Data Source=метнп;Initial Catalog=Demo;Integrated Security=True";

            [TestMethod]
            public void AddAndGetByID_ShouldAddMessageAndReturnCorrectMessage()
            {
             
                DatabaseManager dbManager = new DatabaseManager(ConnectionString);
                Message testMessage = new Message { ID = 1, Name = "TestUser", MessageText = "TestMessage" };

              
                dbManager.Add(testMessage);
                Message retrievedMessage = dbManager.GetByID(1);

              
                Assert.IsNotNull(retrievedMessage);
                Assert.AreEqual(testMessage.ID, retrievedMessage.ID);
                Assert.AreEqual(testMessage.Name, retrievedMessage.Name);
                Assert.AreEqual(testMessage.MessageText, retrievedMessage.MessageText);

                dbManager.Delete(1);
            }

            [TestMethod]
            public void GetByName_ShouldReturnCorrectMessages()
            {
         
                DatabaseManager dbManager = new DatabaseManager(ConnectionString);
                Message testMessage1 = new Message { ID = 2, Name = "TestUser1", MessageText = "TestMessage1" };
                Message testMessage2 = new Message { ID = 3, Name = "TestUser2", MessageText = "TestMessage2" };

          
                dbManager.Add(testMessage1);
                dbManager.Add(testMessage2);
                var retrievedMessages = dbManager.GetByName("TestUser");

        
                Assert.IsNotNull(retrievedMessages);
                Assert.AreEqual(2, retrievedMessages.Count());
                Assert.IsTrue(retrievedMessages.Any(m => m.ID == testMessage1.ID));
                Assert.IsTrue(retrievedMessages.Any(m => m.ID == testMessage2.ID));


                dbManager.Delete(2);
                dbManager.Delete(3);
            }

            [TestMethod]
            public void Update_ShouldUpdateMessageText()
            {
       
                DatabaseManager dbManager = new DatabaseManager(ConnectionString);
                Message testMessage = new Message { ID = 4, Name = "TestUser", MessageText = "OriginalText" };

        
                dbManager.Add(testMessage);
                dbManager.Update(4, "UpdatedText");
                Message updatedMessage = dbManager.GetByID(4);

      
                Assert.IsNotNull(updatedMessage);
                Assert.AreEqual("UpdatedText", updatedMessage.MessageText);

        
                dbManager.Delete(4);
            }

            [TestMethod]
            public void Delete_ShouldRemoveMessage()
            {
           
                DatabaseManager dbManager = new DatabaseManager(ConnectionString);
                Message testMessage = new Message { ID = 5, Name = "TestUser", MessageText = "TestMessage" };


                dbManager.Add(testMessage);
                dbManager.Delete(5);
                Message deletedMessage = dbManager.GetByID(5);

                Assert.IsNull(deletedMessage);
            }
        }
    }

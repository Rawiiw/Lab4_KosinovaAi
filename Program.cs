using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using lab4_KosinovaAiLib;

class Program
{
    static void Main()
    {
        string url = "https://www.cyberforum.ru/csharp-beginners/thread2787061.html";
        string connectionString = @"Data Source=НЕФОР;Initial Catalog=Demo;Integrated Security=True";

        DatabaseManager dbManager = new DatabaseManager(connectionString);
        ParseAndSaveData(url, dbManager);
    }

    static void ParseAndSaveData(string url, DatabaseManager dbManager)
    {
        try
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(url);

                var messages = driver.FindElements(By.XPath("//div[@class='message']"));
                if (messages != null)
                {
                    foreach (var message in messages)
                    {
                        var usernameNode = message.FindElement(By.XPath(".//a[@class='bigusername']"));
                        var commentTextNode = message.FindElement(By.XPath(".//div[@class='postbody']"));

                        if (usernameNode != null && commentTextNode != null)
                        {
                            var username = usernameNode.Text.Trim();
                            var commentText = commentTextNode.Text.Trim();

                            string messageId = System.Web.HttpUtility.ParseQueryString(new Uri(url).Query)["p"];

                            dbManager.Add(new Message { ID = Convert.ToInt32(messageId), Name = username, MessageText = commentText });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при парсинге и сохранении данных: {ex.Message}");
        }
    }
}

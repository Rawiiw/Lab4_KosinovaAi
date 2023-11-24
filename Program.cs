using System;
using HtmlAgilityPack;
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
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            var messages = doc.DocumentNode.SelectNodes("//div[@class='message']");
            if (messages != null)
            {
                foreach (var message in messages)
                {
                    var usernameNode = message.SelectSingleNode(".//a[@class='bigusername']");
                    var commentTextNode = message.SelectSingleNode(".//div[@class='postbody']");

                    if (usernameNode != null && commentTextNode != null)
                    {
                        var username = usernameNode.InnerText.Trim();
                        var commentText = commentTextNode.InnerText.Trim();

                  
                        string messageId = System.Web.HttpUtility.ParseQueryString(new Uri(url).Query)["p"];

                  
                        dbManager.Add(new Message { ID = messageId, Name = username, MessageText = commentText });
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

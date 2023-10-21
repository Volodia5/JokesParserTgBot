using System.Text;
using HtmlAgilityPack;

namespace JokesParserTgBot.Parser;

public class BashorgParser
{
    private const string url = "http://bashorg.org/random";
    private const string xPathEpression = "//div[@class='q']/div[2]";
    private HtmlWeb htmlWeb;

    public BashorgParser()
    {
        htmlWeb = new HtmlWeb();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        htmlWeb.OverrideEncoding = Encoding.GetEncoding("windows-1251");
    }

    public List<string> GetRandomQuotes()
    {
        HtmlDocument document = htmlWeb.Load(url);
        HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(xPathEpression);

        List<string> jokes = new List<string>();

        foreach (HtmlNode node in nodes)
        {
            string innerText = node.InnerHtml;
            
            innerText = innerText.Replace("&quot;", "\"").Replace("<br>", "\n");

            jokes.Add(innerText);
        }

        return jokes;
    }
}
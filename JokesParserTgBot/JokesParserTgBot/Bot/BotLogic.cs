using JokesParserTgBot.DbConnector;
using JokesParserTgBot.Models;
using JokesParserTgBot.Parser;
using Microsoft.EntityFrameworkCore;

namespace JokesParserTgBot.Bot;

public class BotLogic
{
    private JokesParserTgBotVododyaDbContext _dbContext;
    private BashorgParser _bashorgParser;
    private Random _random;

    public BotLogic()
    {
        _dbContext = new JokesParserTgBotVododyaDbContext();
        _bashorgParser = new BashorgParser();
        _random = new Random();
    }

    public void ParseAndSaveNewJokes()
    {
        List<String> quotesTexts = _bashorgParser.GetRandomQuotes();
        List<Joke> jokes = new List<Joke>();

        foreach (string text in quotesTexts)
        {
            jokes.Add(item: new Joke() { QuoteText = text });
        }

        _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE jokes");
        _dbContext.SaveChanges();

        _dbContext.Jokes.AddRange(jokes);
        _dbContext.SaveChanges();
    }

    public string GetRandomJoke()
    {
        List<Joke> jokes = _dbContext.Jokes.ToList();
        if (jokes.Count != 0)
        {
            Joke joke = jokes[_random.Next(0, jokes.Count())];
            return joke.QuoteText;
        }

        return "Список шуток пуст, пожалуйста загрузите новые шутки.";
    }
}
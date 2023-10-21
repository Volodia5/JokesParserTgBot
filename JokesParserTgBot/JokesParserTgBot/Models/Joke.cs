using System;
using System.Collections.Generic;

namespace JokesParserTgBot.Models;

public partial class Joke
{
    public int Id { get; set; }

    public string QuoteText { get; set; } = null!;
}

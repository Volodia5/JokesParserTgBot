using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace JokesParserTgBot.Bot;

public class BotInitializer
{
    private TelegramBotClient _botClient;
    private CancellationTokenSource _cancellationTokenSource;

    public BotInitializer()
    {
        _botClient = new TelegramBotClient("6614269872:AAHwkiMnSAPu-AZOPz3w4VtwL0BgelLJ32M");
        _cancellationTokenSource = new CancellationTokenSource();

        Console.WriteLine("Выполнена инициализация Бота");
    }

    public void Start()
    {
        ReceiverOptions receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        BotRequestHandlers botRequestHandlers = new BotRequestHandlers();

        _botClient.StartReceiving(
            botRequestHandlers.HandleUpdateAsync,
            botRequestHandlers.HandlePollingErrorAsync,
            receiverOptions,
            _cancellationTokenSource.Token
        );

        Console.WriteLine("Бот запущен");
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();

        Console.WriteLine("Бот остановлен");
    }
}
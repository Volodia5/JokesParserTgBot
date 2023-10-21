using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace JokesParserTgBot.Bot;

public class BotRequestHandlers
{
    private BotLogic _botLogic;

    public BotRequestHandlers()
    {
        _botLogic = new BotLogic();
    }
    
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        long chatId = 0;
        int messageFromUserId = 0;
        string inputTextData = "";
        string outputTextData = "";

        try
        {
            if (update.Type == UpdateType.Message)
            {
                chatId = update.Message.Chat.Id;
                messageFromUserId = update.Message.MessageId;
                inputTextData = update.Message.Text;

                Console.WriteLine(
                    $"ВХОДЯЩЕЕ СОООБЩЕНИЕ chatId = {chatId}; messageId = {messageFromUserId}; text = {inputTextData} ");

                if (inputTextData == "/start")
                {
                    outputTextData = "Вечер в хату";
                }
                else if (inputTextData == "/loadjokes")
                {
                    outputTextData = "Новые шутки успешно загружены";
                    _botLogic.ParseAndSaveNewJokes();
                }
                else if (inputTextData == "/randomjoke")
                {
                    outputTextData = _botLogic.GetRandomJoke();
                }
                else
                {
                    outputTextData = "Неизвестная команда";
                }
                
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: outputTextData,
                    cancellationToken: cancellationToken
                );
            }
        }
        catch (Exception e)
        {
            await botClient.DeleteMessageAsync(
                chatId: chatId,
                messageId: messageFromUserId,
                cancellationToken: cancellationToken
            );

            Console.WriteLine($"ОШИБКА chatId = {chatId}; text = {e.Message}");
        }
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine($"Ошибка поймана в методе HandlePollingErrorAsync, {errorMessage}");
        return Task.CompletedTask;
    }
}
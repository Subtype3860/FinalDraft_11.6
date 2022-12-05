using FinalDraft_11._6.Controller;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FinalDraft_11._6;

internal class Bot : BackgroundService
{
    private readonly ITelegramBotClient _telegramClient;
    private readonly TextMessageController _textMessageController;
    private readonly InlineKeyboardController _inlineKeyboardController;

    public Bot(ITelegramBotClient telegramBotClient, TextMessageController textMessageController, InlineKeyboardController inlineKeyboardController)
    {
        _telegramClient = telegramBotClient;
        _textMessageController = textMessageController;
        _inlineKeyboardController = inlineKeyboardController;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _telegramClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions(), // Здесь выбираем, какие обновления хотим получать. В данном случае разрешены все
            stoppingToken);

        Console.WriteLine("Бот запущен");
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            await _inlineKeyboardController.Handle(update.CallbackQuery!, cancellationToken);
            return;
        }

        // Обрабатываем входящие сообщения из Telegram Bot API: https://core.telegram.org/bots/api#message
        if (update.Type == UpdateType.Message)
        {
            if (update.Message!.Type == MessageType.Text)
            {
                await _textMessageController.Handle(update.Message, cancellationToken);
            }
            else
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Сообщение неверного формата",
                cancellationToken: cancellationToken);
            }
        }
            
        
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        // Задаем сообщение об ошибке в зависимости от того, какая именно ошибка произошла
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        // Выводим в консоль информацию об ошибке
        Console.WriteLine(errorMessage);

        // Задержка перед повторным подключением
        Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
        Thread.Sleep(10000);

        return Task.CompletedTask;
    }
}
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FinalDraft_11._6.Controller;

public class InlineKeyboardController
{
    private readonly ITelegramBotClient _telegramClient;

    public InlineKeyboardController(ITelegramBotClient telegramBotClient)
    {
        _telegramClient = telegramBotClient;
    }
    public async Task Handle(CallbackQuery callbackQuery, CancellationToken ct)
    {

            var x = callbackQuery.Message;
       
    }
}
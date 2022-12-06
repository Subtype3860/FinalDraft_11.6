using FinalDraft_11._6.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
        switch (callbackQuery.Data)
        {
            case "t":
                var t = callbackQuery.Message!.Text!.Replace("\n", "").Length;
                await _telegramClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                    $"Число символов в строке: {t}", cancellationToken: ct);
                return;
            case "f":
                var f = callbackQuery.Message!.Text!.Split(' ');
                int j = 0;
                foreach (var i in f)
                {
                    if (!int.TryParse(i, out j))
                    {
                        await _telegramClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                            $"Значение {i} не является допустимым{Environment.NewLine}при сложении простых чисел", 
                            cancellationToken: ct, 
                            parseMode: ParseMode.Html);
                        j = 0;
                        return;
                    }

                    j += int.Parse(i);
                }

                await _telegramClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Сумма чисел ровна: {j}",
                    cancellationToken: ct);
                return;

        }



    }
}
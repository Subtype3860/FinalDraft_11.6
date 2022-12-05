using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FinalDraft_11._6.Controller;

public class TextMessageController
{
    private readonly ITelegramBotClient _telegramBotClient;

    public TextMessageController(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async Task Handle(Message message, CancellationToken cancellationToken)
    {
        if (message.Text!.ToLower().Equals("/start"))
        {

            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                $"<b>  Наш бот подсчитывает символы в тексте.</b> {Environment.NewLine}" +
                $"{Environment.NewLine}<b>Производит сложения чисел.</b>{Environment.NewLine}",
                cancellationToken: cancellationToken, parseMode: ParseMode.Html);
        }
        else
        {
            var buttons = new List<InlineKeyboardButton[]>();
            buttons.Add(new[]
            {
                InlineKeyboardButton.WithCallbackData("Подсчёт символов", "t"),
                InlineKeyboardButton.WithCallbackData("Сложение", "f")
            });
            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, 
                $"<i>Вы ввели текст.</i>{Environment.NewLine}<b>{message.Text}</b>{Environment.NewLine}Выберите действия",
                cancellationToken: cancellationToken, parseMode: ParseMode.Html,
                replyMarkup: new InlineKeyboardMarkup(buttons));
        }
    }
}
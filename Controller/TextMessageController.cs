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
        await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Bot  вработе", cancellationToken: cancellationToken);
        switch (message.Text)
        {
            case "/start":
                var ss = message.Text;
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new []
                {
                    InlineKeyboardButton.WithCallbackData("Подсчёт символов", "t"),
                    InlineKeyboardButton.WithCallbackData("Сложение", "f")
                });
                await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                    $"<b>  Наш бот подсчитывает символы в тексте.</b> {Environment.NewLine}" +
                    $"{Environment.NewLine}<b>Производит сложения чисел.</b>{Environment.NewLine}",
                    cancellationToken: cancellationToken, parseMode: ParseMode.Html,
                    replyMarkup: new InlineKeyboardMarkup(buttons));
                return;
            default:
                await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                    "Отправьте аудио для превращения в текст.", cancellationToken: cancellationToken);
                return;
        }
        
    }
}
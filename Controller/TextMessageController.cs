using Telegram.Bot;
using Telegram.Bot.Types;

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
        if (message.Text != string.Empty)
        {
            
        }
    }
}
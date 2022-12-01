using System.Text;
using FinalDraft_11._6.Configuration;
using FinalDraft_11._6.Controller;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace FinalDraft_11._6
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
 
            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем
 
            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var appSetting = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSetting.BotToken!));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }

        private static AppSettings BuildAppSettings()
        {
            return new AppSettings
            {
                BotToken = "5710703333:AAEVW-ZPHTMR4NLTK92Th_s2g3YJmlLfHtA"
            };
        }
    }
}
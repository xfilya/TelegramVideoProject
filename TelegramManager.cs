using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UnityEngine;

public class TelegramManager : MonoBehaviour
{
    private const string TOKEN = "";

    private TelegramBotClient _botClient;
    private readonly CancellationTokenSource _cts = new();

    private async void Start() => await StartBot();
    private void OnDestroy() => StopBot();

    public async Task StartBot()
    {
        _botClient = new TelegramBotClient(token: TOKEN, cancellationToken: _cts.Token);

        var bot = await _botClient.GetMeAsync();
        Debug.Log($"Áîò {bot.Username} óñïåøíî çàïóñòèëñÿ!");

        _botClient.OnMessage += OnMessage;
    }

    private void StopBot()
    {
        _botClient.OnMessage -= OnMessage;

        _cts.Cancel();
        _cts.Dispose();

        Debug.Log($"Áîò óñïåøíî âûêëþ÷èëñÿ.");
    }

    private async Task OnMessage(Message message, UpdateType type)
    {
        if (message.Text == "/start")
        {
            await _botClient.SendTextMessageAsync(message.Chat, $"Ïðèâåò, {message.From.FirstName}! Âû óñïåøíî çàïóñòèëè áîòà.");
            Debug.Log($"Ïðèâåò, {message.From.FirstName}! Âû óñïåøíî çàïóñòèëè áîòà.");
            return;
        }

        await _botClient.SendTextMessageAsync(message.Chat, $"Ïîëüçîâàòåëü {message.From.FirstName} íàïèñàë: '{message.Text}'");
        Debug.Log($"Ïîëüçîâàòåëü {message.From.FirstName} íàïèñàë: '{message.Text}'");
    }
}

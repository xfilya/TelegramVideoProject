using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UnityEngine;

public class TelegramManager : MonoBehaviour
{
    private const string TOKEN = "7512239461:AAF_JWzBv4ZkBRAoLqbAKWiUq5HDh_MwW4k";

    private TelegramBotClient _botClient;
    private readonly CancellationTokenSource _cts = new();

    private async void Start() => await StartBot();
    private void OnDestroy() => StopBot();

    public async Task StartBot()
    {
        _botClient = new TelegramBotClient(token: TOKEN, cancellationToken: _cts.Token);

        var bot = await _botClient.GetMeAsync();
        Debug.Log($"��� {bot.Username} ������� ����������!");

        _botClient.OnMessage += OnMessage;
    }

    private void StopBot()
    {
        _botClient.OnMessage -= OnMessage;

        _cts.Cancel();
        _cts.Dispose();

        Debug.Log($"��� ������� ����������.");
    }

    private async Task OnMessage(Message message, UpdateType type)
    {
        if (message.Text == "/start")
        {
            await _botClient.SendTextMessageAsync(message.Chat, $"������, {message.From.FirstName}! �� ������� ��������� ����.");
            Debug.Log($"������, {message.From.FirstName}! �� ������� ��������� ����.");
            return;
        }

        await _botClient.SendTextMessageAsync(message.Chat, $"������������ {message.From.FirstName} �������: '{message.Text}'");
        Debug.Log($"������������ {message.From.FirstName} �������: '{message.Text}'");
    }
}

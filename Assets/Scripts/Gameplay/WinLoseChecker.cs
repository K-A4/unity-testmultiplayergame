using Fusion;

public class WinLoseChecker : NetworkBehaviour
{
    private WinLoseTextUI winLoseTextUI;
    private AlivePlayerRegistry alivePlayers;
    private GameLoader game;
    private Health playerHealth;
    private CoinCollector coinCollector;
    private PlayerNick nick;

    public override void Spawned()
    {
        playerHealth  = GetComponent<Health>();
        coinCollector = GetComponent<CoinCollector>();
        nick          = GetComponent<PlayerNick>();
        winLoseTextUI = ServiceLocator.Instance.GetService<WinLoseTextUI>();
        alivePlayers  = ServiceLocator.Instance.GetService<AlivePlayerRegistry>();
        game          = ServiceLocator.Instance.GetService<GameLoader>();
        alivePlayers.OnCountChanged += CheckWin;
    }

    private void CheckWin(int alivePlayersCount)
    {
        if (alivePlayersCount == 1 && !playerHealth.isDead && game.state == GameLoader.GameState.Started)
        {
            winLoseTextUI.SetWinText(nick.Name, coinCollector.NumberOfCoins);
        }
        game.SetGameEnded();
    }

    private void OnDestroy()
    {
        alivePlayers.OnCountChanged -= CheckWin;
    }
}

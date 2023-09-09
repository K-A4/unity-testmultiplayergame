using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameLoader : NetworkBehaviour, IPlayerJoined
{
    public List<string> nicknames = new List<string> { "Doobie", "General", "Frogger", "Bandit", "Ghoulie", "Babs", "LaLa", "Coke ", "Toodles", "Rubber", "Tarzan", "Pickles"};

    public static event Action OnGameStart;
    public int PlayerCount => Runner.ActivePlayers.Count();


    [SerializeField] private Text connectionText;
    [SerializeField] private AlivePlayerRegistry playersAlive;
    [SerializeField] private PlayerSpawner playerSpawner;

    [Networked]
    public GameState state { get; set; }

    public override void Spawned()
    {
        if (PlayerCount <= 4 && state != GameState.Ended)
        {
            var netobj = playerSpawner.SpawnPlayerPrefab(PlayerCount, Runner);

            SetPlayerNickname(netobj);

            playersAlive.AddRpc();
        }

        CheckConnectedPlayers();
    }

    private void SetPlayerNickname(NetworkObject netobj)
    {
        if (netobj.TryGetComponent(out PlayerNick nickname))
        {
            nickname.SetNickRpc(nicknames[(int)(UnityEngine.Random.Range(0f, 0.99f) * nicknames.Count)]);
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (PlayerCount > 2)
        {
            return;
        }

        CheckConnectedPlayers();
    }

    private void CheckConnectedPlayers()
    {
        var playerCount = PlayerCount;

        if (playerCount >= 2 && state != GameState.Started)
        {
            StartCoroutine(GameStartCounter());
        }
        else if (state == GameState.Started)
        {
            OnGameStart.Invoke();
        }
    }

    private IEnumerator GameStartCounter()
    {
        state = GameState.Starting;

        connectionText.text = "3";
        yield return new WaitForSeconds(1);
        connectionText.text = "2";
        yield return new WaitForSeconds(1);
        connectionText.text = "1";
        yield return new WaitForSeconds(1);
        connectionText.text = "START";
        yield return new WaitForSeconds(0.5f);
        connectionText.gameObject.SetActive(false);
        state = GameState.Started;

        OnGameStart.Invoke();
    }

    public void SetGameEnded()
    {
        state = GameState.Ended;
    }

    public enum GameState
    {
        Waiting,
        Starting,
        Started,
        Ended
    }
}

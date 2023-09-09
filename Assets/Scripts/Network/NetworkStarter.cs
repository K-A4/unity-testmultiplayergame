using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkStarter : MonoBehaviour
{
    [SerializeField] private NetworkRunner runnerPrefab;
    [SerializeField] private int sceneIndex;

    public IEnumerator LoadingConnection(string roomName)
    {
        var clientsStartTask = AddClient(GameMode.Shared, sceneIndex, roomName);

        while (clientsStartTask.IsCompleted == false)
        {
            yield return new WaitForSeconds(1f);
        }

        yield break;
    }

    public Task AddClient(GameMode serverMode, SceneRef sceneRef, string roomName)
    {
        var client = Instantiate(runnerPrefab);
        DontDestroyOnLoad(client);

        client.name = $"Client {(Char)(65)}";

        var clientTask = InitializeNetworkRunner(client, serverMode, NetAddress.Any(), sceneRef, roomName);

        return clientTask;
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress address, SceneRef scene, string roomName)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
        if (sceneManager == null)
        {
            Debug.Log($"NetworkRunner does not have any component implementing {nameof(INetworkSceneManager)} interface, adding {nameof(NetworkSceneManagerDefault)}.", runner);
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = address,
            Scene = scene,
            SessionName = roomName,
            Initialized = null,
            SceneManager = sceneManager
        });
    }
}

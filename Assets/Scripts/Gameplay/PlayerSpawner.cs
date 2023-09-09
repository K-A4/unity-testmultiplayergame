using UnityEngine;
using Fusion;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    public NetworkObject SpawnPlayerPrefab(int pointIndex, NetworkRunner runner)
    {
        var next = pointIndex++ % transform.childCount;
        var spawnPosition = transform.GetChild(next).position;
        return runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity);
    }
}

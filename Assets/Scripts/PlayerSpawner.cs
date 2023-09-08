using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            var next = player % transform.childCount;
            var spawnPosition = transform.GetChild(next).position;
            Runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
        }
    }
}

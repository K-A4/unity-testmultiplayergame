using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CoinSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float spawnRadius;
    [SerializeField]
    private int numberOfCoins;
    [Networked]
    private int coinsSpawned { get; set; }

    public override void Spawned()
    {
        while (coinsSpawned < numberOfCoins)
        {
            Runner.Spawn(coinPrefab, Random.insideUnitCircle * spawnRadius);
            coinsSpawned++;
        }
    }
}

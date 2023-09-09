using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Coin : NetworkBehaviour, ICollectible
{
    public void Collect()
    {
        gameObject.SetActive(false);
        Runner.Despawn(GetComponent<NetworkObject>());
    }
}

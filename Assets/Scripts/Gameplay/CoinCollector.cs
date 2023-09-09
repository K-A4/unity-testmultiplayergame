using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fusion;

public class CoinCollector : NetworkBehaviour
{
    public UnityEvent OnCoinsChanged;
    public int NumberOfCoins { get; private set; }

    [SerializeField] private float collectRadius;
    [SerializeField] private LayerMask collectLayer;

    public override void Spawned()
    {
        InvokeCoinsChanged();
    }

    public override void FixedUpdateNetwork()
    {
        var collider = Runner.GetPhysicsScene2D().OverlapCircle(transform.position, collectRadius, collectLayer);

        if (collider)
        {
            CollectFromCollider(collider);
        }
    }

    private void CollectFromCollider(Collider2D other)
    {
        if (other.transform.TryGetComponent(out Coin coin))
        {
            coin.Collect();
            NumberOfCoins++;
            InvokeCoinsChanged();
        }
    }

    private void InvokeCoinsChanged()
    {
        if (HasStateAuthority)
        {
            OnCoinsChanged?.Invoke();
        }
    }
}

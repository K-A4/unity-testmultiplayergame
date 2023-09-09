using UnityEngine;
using UnityEngine.Events;
using Fusion;

public class Health : NetworkBehaviour
{
    public UnityEvent<float> OnTakeDamage;
    public bool isDead { get; private set; }

    [SerializeField] private float maxHealth;
    private AlivePlayerRegistry playersAlive;

    [Networked]
    private float healthPoints { get; set; }


    public override void Spawned()
    {
        healthPoints = maxHealth;
        isDead = false;
        playersAlive = ServiceLocator.Instance.GetService<AlivePlayerRegistry>();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void TakeDamageRpc(float damage)
    {
        if (isDead)
        {
            return;
        }

        healthPoints -= damage;
        OnTakeDamage.Invoke(healthPoints / maxHealth);

        if (healthPoints <= 0)
        {
            isDead = true;

            if (HasStateAuthority)
            {
                playersAlive.RemoveRpc();
            }
            Runner.Despawn(gameObject.GetComponent<NetworkObject>());
        }
    }

    private void OnDestroy()
    {
        if (HasStateAuthority)
        {
            playersAlive.RemoveRpc();
        }
    }
}

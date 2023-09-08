using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float bulletDistance;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;

    private float timeToDead;

    public override void Spawned()
    {
        if (HasStateAuthority ==  false)
        {
            return;
        }
        timeToDead = lifeTime;
    }

    public override void FixedUpdateNetwork()
    {
        CheckBulletOverlap();

        if (Runner)
        {
            timeToDead -= Runner.DeltaTime;

            transform.position += transform.up * bulletSpeed * Runner.DeltaTime;

            if (timeToDead < 0)
            {
                Runner.Despawn(GetComponent<NetworkObject>());
            }
        }
    }

    private void CheckBulletOverlap()
    {
        var hit = Runner.GetPhysicsScene2D().Raycast(transform.position, transform.up, bulletDistance);
        if (hit.transform)
        {
            if (hit.transform.TryGetComponent(out Health health))
            {
                health.TakeDamage(bulletDamage);
                Runner.Despawn(GetComponent<NetworkObject>());
            }
        }
    }
}

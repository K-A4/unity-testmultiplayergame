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

    private bool isHit = false;
    private float timeToDead;

    public override void Spawned()
    {
        timeToDead = lifeTime;
    }

    public override void FixedUpdateNetwork()
    {
        CheckBulletOverlap();

        if (!isHit)
        {
            timeToDead -= Runner.DeltaTime;

            transform.position += transform.up * bulletSpeed * Runner.DeltaTime;

            if (timeToDead < 0)
            {
                DestroyBullet();
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
                health.TakeDamageRpc(bulletDamage);
                DestroyBullet();
                isHit = true;
            }
        }
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
        Runner.Despawn(GetComponent<NetworkObject>());
    }
}

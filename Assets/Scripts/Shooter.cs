using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Shooter : NetworkBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float firePeriod;

    private float timeElapsed;

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
        {
            return;
        }
        timeElapsed += Runner.DeltaTime;
        if (timeElapsed > firePeriod)
        {
            Fire();
            timeElapsed = 0;
        }
    }

    public void Fire()
    {
        Runner.Spawn(bulletPrefab, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z));
    }
}

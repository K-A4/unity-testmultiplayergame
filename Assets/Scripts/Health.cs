using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fusion;

public class Health : NetworkBehaviour
{
    public UnityEvent<float> OnTakeDamage;

    [SerializeField] private float maxHealth;

    [Networked]
    private float healthPoints { get; set; }

    private bool isDead;
    public override void Spawned()
    {
        healthPoints = maxHealth;
        isDead = false;
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        healthPoints -= damage;
        OnTakeDamage.Invoke(healthPoints / maxHealth);

        Debug.Log(Runner.name + " HP: " + healthPoints);

        if (healthPoints < 0)
        {
            isDead = true;
            Runner.Despawn(gameObject.GetComponent<NetworkObject>());
        }
    }
}

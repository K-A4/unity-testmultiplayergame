using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerColor : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer directionSprite;

    public override void Spawned()
    {
        directionSprite.color = Random.ColorHSV();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerColor : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer directionSprite;

    [Networked]
    private Color color { get; set; }

    public override void Spawned()
    {
        color = Random.ColorHSV();
        directionSprite.color = color;
    }
}

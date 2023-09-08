using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private Transform view;

    private Vector2 goalVelocity;
    private Rigidbody2D rigidBody;

    public override void Spawned()
    {
        rigidBody = GetComponent<Rigidbody2D>(); 
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
        {
            return;
        }
        var inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputVector = Vector2.ClampMagnitude(inputVector, 1.0f);

        HandleMovement(inputVector);

        if (inputVector != Vector2.zero)
        {
            RotateToMoveVector();
        }
    }

    private void HandleMovement(Vector2 inputVector)
    {
        var moveVector = inputVector;
        moveVector *= maxMoveSpeed;

        goalVelocity = Vector2.MoveTowards(goalVelocity, moveVector, acceleration * Runner.DeltaTime);

        var neededAcceleration = (goalVelocity - rigidBody.velocity) / Runner.DeltaTime;

        rigidBody.AddForce(neededAcceleration);
    }

    private void RotateToMoveVector()
    {
        var angle = Mathf.Atan2(goalVelocity.x, goalVelocity.y) * Mathf.Rad2Deg;
        rigidBody.rotation = -angle;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBinder : MonoBehaviour
{
    [SerializeField] private VirtualGamepad gamepad;

    private void Start()
    {
        BindVirtualGamepad();
    }

    private void BindVirtualGamepad()
    {
        ServiceLocator.Instance.SetService<VirtualGamepad>(gamepad);
    }
}

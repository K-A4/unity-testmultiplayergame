using System;
using Fusion;

public class AlivePlayerRegistry : NetworkBehaviour
{
    public event Action<int> OnCountChanged;

    [Networked]
    private int Count { get; set; }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void AddRpc()
    {
        Count++;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RemoveRpc()
    {
        Count--;
    }

    private int prevCoint;

    public override void FixedUpdateNetwork()
    {
        if (prevCoint != Count)
        {
            OnCountChanged?.Invoke(Count);
        }
        prevCoint = Count;
    }
}

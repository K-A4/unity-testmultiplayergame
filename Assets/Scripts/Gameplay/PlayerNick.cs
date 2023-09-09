using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class PlayerNick : NetworkBehaviour
{
    [SerializeField] private Text nicknameText;
    [HideInInspector]
    [Networked]
    public string Name { get; set; }

    public override void Spawned()
    {
        nicknameText.text = Name;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void SetNickRpc(string nickname)
    {
        Name = nickname;
        nicknameText.text = Name;
    }
}

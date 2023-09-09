using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Fusion;
using Fusion.Sockets;
using System;
using System.Threading.Tasks;
using System.Linq;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private NetworkStarter networkStart;
    [SerializeField] private Button createButton;
    [SerializeField] private Button enterButton;
    [SerializeField] private InputField createRoomName;
    [SerializeField] private InputField enterRoomName;

    private void Start()
    {
        createButton.onClick.AddListener(CreateRoom);
        enterButton.onClick.AddListener(EnterRoom);
    }

    private void CreateRoom()
    {
        StartCoroutine(networkStart.LoadingConnection(createRoomName.text));
    }

    private void EnterRoom()
    {
        StartCoroutine(networkStart.LoadingConnection(enterRoomName.text));
    }

    private void OnDestroy()
    {
        createButton.onClick.RemoveAllListeners();
        enterButton.onClick.RemoveAllListeners();
    }
}

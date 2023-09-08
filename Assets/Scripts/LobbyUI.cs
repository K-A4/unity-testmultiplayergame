using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Fusion;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private NetworkDebugStart networkStart;
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
        networkStart.DefaultRoomName = createRoomName.text;
        networkStart.StartSharedClient();
    }

    private void EnterRoom()
    {
        networkStart.DefaultRoomName = enterRoomName.text;
        networkStart.StartSharedClient();
    }

    private void OnDestroy()
    {
        createButton.onClick.RemoveAllListeners();
        enterButton.onClick.RemoveAllListeners();
    }
}

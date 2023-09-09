using UnityEngine;

public class ComponentBinder : MonoBehaviour
{
    [SerializeField] private VirtualGamepad gamepad;
    [SerializeField] private CoinCountUI coinUI;
    [SerializeField] private AlivePlayerRegistry playersAlive;
    [SerializeField] private WinLoseTextUI wniLoseText;
    [SerializeField] private GameLoader game;

    private void Start()
    {
        BindVirtualGamepad();
        BindUI();
        ServiceLocator.Instance.SetService<AlivePlayerRegistry>(playersAlive);
        ServiceLocator.Instance.SetService<GameLoader>(game);
    }

    private void BindVirtualGamepad()
    {
        ServiceLocator.Instance.SetService<VirtualGamepad>(gamepad);
    }

    private void BindUI()
    {
        ServiceLocator.Instance.SetService<CoinCountUI>(coinUI);
        ServiceLocator.Instance.SetService<WinLoseTextUI>(wniLoseText);
    }
}

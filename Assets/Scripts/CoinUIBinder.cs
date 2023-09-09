using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUIBinder : MonoBehaviour
{
    [SerializeField] private CoinCollector coinCollector;
    private CoinCountUI coinUI;

    private void Start()
    {
        coinUI = ServiceLocator.Instance.GetService<CoinCountUI>();
        coinCollector.OnCoinsChanged.AddListener(ChangeUIText);
    }

    private void ChangeUIText()
    {
        coinUI.SetText(coinCollector.NumberOfCoins.ToString());
    }

    private void OnDestroy()
    {
        coinCollector.OnCoinsChanged.RemoveListener(ChangeUIText);
    }
}

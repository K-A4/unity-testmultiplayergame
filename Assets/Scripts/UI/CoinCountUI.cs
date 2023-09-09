using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountUI : MonoBehaviour
{
    [SerializeField] private Text uiText;

    public void SetText(string text)
    {
        uiText.text = text;
    }
}

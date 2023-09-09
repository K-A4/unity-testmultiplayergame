using UnityEngine;
using UnityEngine.UI;

public class WinLoseTextUI : MonoBehaviour
{
    [SerializeField] private Text winLoseText;

    public void SetWinText(string playerName, int coinCount)
    {
        winLoseText.text = $"{playerName} Wins!\n With {coinCount} coins";
    }
}

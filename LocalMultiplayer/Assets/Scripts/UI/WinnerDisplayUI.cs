using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinnerDisplayUI: MonoBehaviour
{
  [SerializeField] private TMP_Text _winnerText;
  [SerializeField] private Image _winnerImage;

  private void Start()
  {
    _winnerText.text = "Player " + GameManager.instance.RoundWinnerPlayerNumber + " wins";

    if(GameManager.instance.WinnerSprite == null) { return; }
    _winnerImage.sprite = GameManager.instance.WinnerSprite;
  }
}
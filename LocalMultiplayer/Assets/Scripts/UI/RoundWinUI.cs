using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;

public class RoundWinUI : MonoBehaviour
{
  [SerializeField] private TMP_Text _winnerText;
  [SerializeField] private TMP_Text _countDownText;

  private IArenaLoadTrigger _arenaLoadTrigger;
  private IEnumerator _nextArenaLoadCounterCoroutine;

  private void Awake()
  {
    GameManager.instance.GameEndEvent += OnRoundOwer;

    _arenaLoadTrigger = FindObjectsOfType<MonoBehaviour>().OfType<IArenaLoadTrigger>().SingleOrDefault();

    if (_arenaLoadTrigger != null)
      _arenaLoadTrigger.ArenaLoadEvent += OnRoundOwer;
    
    gameObject.SetActive(false);
  }

  private void OnDestroy()
  {
    GameManager.instance.GameEndEvent -= OnRoundOwer;
    if (_arenaLoadTrigger != null)
      _arenaLoadTrigger.ArenaLoadEvent -= OnRoundOwer;
  }

  private void OnRoundOwer(int timeToLoad)
  {
    gameObject.SetActive(true);
    _countDownText.gameObject.SetActive(false);
    _winnerText.text = "Player " + GameManager.instance.RoundWinnerPlayerNumber + " won";
    _nextArenaLoadCounterCoroutine = CountDownCounter(timeToLoad);
    StartCoroutine(_nextArenaLoadCounterCoroutine);
  }

  IEnumerator CountDownCounter(int timeToCount)
  {
    for (int i = timeToCount; i > 0; i--)
    {
      yield return new WaitForSeconds(1f);
      _countDownText.gameObject.SetActive(true);
      _countDownText.text = i.ToString();
      AudioManager.instance.PlaySound(Sound.Type.Countdown);
    }
  }
}

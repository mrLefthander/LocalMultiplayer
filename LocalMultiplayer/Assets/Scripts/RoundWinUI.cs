using System.Linq;
using UnityEngine;
using TMPro;

public class RoundWinUI : MonoBehaviour
{
  [SerializeField] private TMP_Text _winnerText;

  private IArenaLoadTrigger _arenaLoadTrigger;

  private void Awake()
  {
    _arenaLoadTrigger = FindObjectsOfType<MonoBehaviour>().OfType<IArenaLoadTrigger>().SingleOrDefault();
    if (_arenaLoadTrigger != null)
    {
      _arenaLoadTrigger.ArenaLoadEvent += OnRoundOwer;
    }
    
    gameObject.SetActive(false);
  }

  private void OnDestroy()
  {
    if (_arenaLoadTrigger != null)
    {
      _arenaLoadTrigger.ArenaLoadEvent -= OnRoundOwer;
    }
  }

  private void OnRoundOwer(int timeToLoad)
  {
    gameObject.SetActive(true);
    _winnerText.text = "Player " + ApplicationVariables.LastRoundWinnerName + " won";
  }
}

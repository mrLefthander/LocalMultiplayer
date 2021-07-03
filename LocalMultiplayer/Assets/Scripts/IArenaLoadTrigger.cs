using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IArenaLoadTrigger
{
  public event UnityAction<int> ArenaLoadEvent;
  public event UnityAction ArenaLoadCanceledEvent;
}

using System;
using UnityEngine;

namespace GameTemplate {

  public abstract class PersistentSingletonBehaviour<S, T>
    : SingletonBehaviour<S, T>
    where T : MonoBehaviour
  {

    protected override void Start() {
      base.Start();
      DontDestroyOnLoad(gameObject);
    }
  }
}

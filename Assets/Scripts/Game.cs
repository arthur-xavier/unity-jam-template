using System.Collections;
using UnityEngine;

namespace GameTemplate {

  public abstract class Game<S> : PersistentSingletonBehaviour<Game<S>> {

    [SerializeField]
    [HideInInspector]
    private GameState<S> m_State;
    public GameState<S> State {
      private get { return m_State; }
      set {
        StartCoroutine(ChangeState(value));
      }
    }

    [SerializeField]
    public S Settings { get; protected set; }

    protected override void Start() {
      base.Start();
      LoadSettings();
    }

    protected abstract void LoadSettings();
    protected abstract void SaveSettings();

    private IEnumerator ChangeState(GameState<S> state) {
      if (m_State != null) {
        yield return StartCoroutine(m_State.OnStateExit());
      }
      m_State = state;
      yield return StartCoroutine(state.OnStateEnter());
    }
  }
}

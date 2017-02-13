using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplate {

  /// <summary>
  /// Stock Unity wait rountines allocate memory each invocation causing GC.
  /// These routines are cached globally.
  /// </summary>
  /// <remarks>
  /// original idea: http://forum.unity3d.com/threads/c-coroutine-waitforseconds-garbage-collection-tip.224878/
  /// credits to: https://github.com/robertwahler/jammer/blob/master/Assets/Jammer/Application/WaitFor.cs
  /// </remarks>
  public static class WaitFor {

    private static WaitForEndOfFrame s_EndOfFrame = new WaitForEndOfFrame();

    /// <summary>
    /// Replaces: yield return new WaitForEndOfFrame()
    ///
    /// Usage:
    ///
    /// yield return WaitFor.EndOfFrame;
    /// </summary>
    public static WaitForEndOfFrame EndOfFrame {
      get { return s_EndOfFrame; }
    }

    private static WaitForFixedUpdate s_FixedUpdate = new WaitForFixedUpdate();

    /// <summary>
    /// Replaces: yield return new WaitForFixedUpdate()
    ///
    /// Usage:
    ///
    /// yield return WaitFor.FixedUpdate;
    public static WaitForFixedUpdate FixedUpdate {
      get { return s_FixedUpdate; }
    }

    private const int DICTIONARY_START_SIZE = 50;

    /// <summary>
    /// Cache the Second() methods using the float time as key
    /// </summary>
    private static Dictionary<float, WaitForSeconds> s_Seconds = new Dictionary<float, WaitForSeconds>(DICTIONARY_START_SIZE);

    /// <summary>
    /// Cached waiting
    ///
    /// Usage:
    ///
    /// yield return WaitFor.Seconds(5.0f);
    /// </summary>
    public static WaitForSeconds Seconds(float seconds) {
      if (!s_Seconds.ContainsKey(seconds)) {
        s_Seconds.Add(seconds, new WaitForSeconds(seconds));
        if (s_Seconds.Count > DICTIONARY_START_SIZE)
          Debug.LogWarning(string.Format("WaitFor.WaitForSeconds(seconds: {0}) allocated extra dictionary slots. Problem? If not, increase starting size", seconds));
      }

      return s_Seconds[seconds];
    }
  }
}

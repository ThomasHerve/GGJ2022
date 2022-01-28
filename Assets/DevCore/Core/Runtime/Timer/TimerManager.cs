namespace DevCore.Core {
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// The timer updater system
    /// Must not be destroyed
    /// </summary>
    [DefaultExecutionOrder(-500)]
    public class TimerManager : MonoBehaviour {
        #region Currents
        private static TimerManager instance = null;
        #endregion

        #region Properties
        private List<Timer> timers = new List<Timer>();
        #endregion

        #region Init
        [RuntimeInitializeOnLoadMethod]
        private static void Init() {
            //Create timer handle on run application
            GameObject go = new GameObject("Timer Handle");
            instance = go.AddComponent<TimerManager>();
            DontDestroyOnLoad(go);
        }
        #endregion

        #region Callbacks
        private void Update() {
            UpdateTimers();
        }
        #endregion

        #region Timers
        /// <summary>
        /// Add a timer to the list
        /// </summary>
        /// <param name="safe">Check if the timer is already registered</param>
        internal static void RegisterTimer(Timer timer, bool safe = true) {
            if (safe) {
                if (!instance.timers.Contains(timer)) {
                    instance.timers.Add(timer);
                }
            } else {
                instance.timers.Add(timer);
            }
        }

        internal static void UnregisterTimer(Timer timer) {
            instance.timers.Remove(timer);
        }

        /// <summary>
        /// Update all timers current time
        /// </summary>
        private void UpdateTimers() {
            for (int i = timers.Count - 1; i >= 0; i--) {
                if (timers[i].isPlaying) {
                    timers[i].Update();
                }
            }
        }
        #endregion
    }
}
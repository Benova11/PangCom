using System;
using UnityEngine;

namespace Utils.Timer
{
    public class BasicTimer : ITimer
    {
        #region Fields

        private int _onBackgroundTime;

        #endregion

        #region Properties

        public bool IsRunning { private set; get; }
        protected bool IsPaused { private set; get; } = true;
        public int CurrentTime { protected set; get; }

        #endregion

        #region Events

        public event Action<int> TimerTick;

        #endregion

        #region Methods

        public virtual void StartTimer(int timeToCountInSeconds = 0)
        {
            IsPaused = false;
            IsRunning = true;
        }

        public virtual void StopTimer()
        {
            IsPaused = true;
            IsRunning = false;
        }

        public virtual void PauseTimer()
        {
            IsPaused = true;
        }

        public virtual void ContinueTimer()
        {
            IsPaused = false;
        }

        protected void NotifyOnTimerTick()
        {
            TimerTick?.Invoke(CurrentTime);
        }

        protected virtual void OnApplicationPause(bool pauseStatus)
        {
            if (!IsRunning || IsPaused) return;

            if (pauseStatus)
            {
                _onBackgroundTime = (int)Time.realtimeSinceStartup;
            }
            else
            {
                _onBackgroundTime = (int)Time.realtimeSinceStartup - _onBackgroundTime;
                CurrentTime -= CurrentTime - _onBackgroundTime > 0 ? _onBackgroundTime : CurrentTime;
            }
        }

        protected virtual void OnDestroy()
        {
            TimerTick = null;
        }

        #endregion
    }
}
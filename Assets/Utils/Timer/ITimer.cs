using System;

namespace Utils.Timer
{
    public interface ITimer
    {
        #region Events

        public event Action<int> TimerTick;

        #endregion

        #region Methods

        public void StartTimer(int timeToCountInSeconds = 0);
        public void StopTimer();
        public void PauseTimer();
        public void ContinueTimer();

        #endregion
    }
}
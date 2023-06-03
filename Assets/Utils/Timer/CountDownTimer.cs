using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utils.Timer
{
    public class CountDownTimer : BasicTimer
    {
        #region Fields

        private CancellationTokenSource _cancellationToken;

        #endregion

        #region Events

        public event Action TimesUp;

        #endregion

        #region Properties

        public bool IsTimesUp { get; private set; }

        #endregion

        #region Constructors

        public CountDownTimer()
        {
        }

        public CountDownTimer(int timeToCountInSeconds)
        {
            CurrentTime = timeToCountInSeconds;
        }

        #endregion

        public override void StartTimer(int timeToCountInSeconds)
        {
            StopTimer();

            IsTimesUp = false;

            CurrentTime = timeToCountInSeconds > 0 ? timeToCountInSeconds : CurrentTime;

            if (CurrentTime == 0)
            {
                OnTimesUp();
            }
            else
            {
                SetToken();
                RunTimer();
                NotifyOnTimerTick();
            }

            base.StartTimer(timeToCountInSeconds);
        }

        private void SetToken()
        {
            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }

        public override void StopTimer()
        {
            if (!IsRunning) return;

            if (_cancellationToken is { IsCancellationRequested: false })
            {
                _cancellationToken.Cancel();
            }

            base.StopTimer();
        }

        private async UniTask RunTimer()
        {
            while (CurrentTime > 0 && !_cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(1000, DelayType.DeltaTime, PlayerLoopTiming.Update, _cancellationToken.Token);

                if (IsPaused || CurrentTime <= 0) continue;

                CurrentTime -= 1;
                NotifyOnTimerTick();
            }

            if (CurrentTime <= 0)
            {
                OnTimesUp();
            }
        }

        private void OnTimesUp()
        {
            if (IsTimesUp) return;

            StopTimer();
            IsTimesUp = true;
            TimesUp?.Invoke();

            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
        }

        protected override void OnApplicationPause(bool pauseStatus)
        {
            base.OnApplicationPause(pauseStatus);

            if (!IsRunning || CurrentTime != 0 || IsPaused || IsTimesUp) return;

            NotifyOnTimerTick();
            OnTimesUp();
        }

        protected override void OnDestroy()
        {
            StopTimer();
            TimesUp = null;
            base.OnDestroy();
        }
    }
}
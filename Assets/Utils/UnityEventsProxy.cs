using System;

namespace Utils
{
    public static class UnityEventsProxy
    {
        #region Events

        public static event Action<bool> OnApplicationToggledPause;

        #endregion

        #region Methods

        private static void OnApplicationPause(bool pauseStatus)
        {
            OnApplicationToggledPause?.Invoke(pauseStatus);
        }

        #endregion
    }
}
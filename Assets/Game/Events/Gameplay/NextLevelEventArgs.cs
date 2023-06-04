using System;

namespace Game.Events
{
    public class NextLevelEventArgs : EventArgs
    {
        private readonly int _currentLevelIndex;

        public int CurrentLevelIndex => _currentLevelIndex;

        public NextLevelEventArgs(int currentLevelIndex)
        {
            _currentLevelIndex = currentLevelIndex;
        }
    }
}
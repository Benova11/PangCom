using System;
using Game.Scripts;

namespace Game.Events
{
    public class PlayerDeadEventArgs : EventArgs
    {
        private readonly Player _deadPlayer;

        public Player DeadPlayer => _deadPlayer;

        public PlayerDeadEventArgs(Player deadPlayer)
        {
            _deadPlayer = deadPlayer;
        }
    }
}
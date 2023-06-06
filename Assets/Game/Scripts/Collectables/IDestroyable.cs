using System;

namespace Game.Scripts.Collectables
{
    public interface IDestroyable
    {
        public void DestroySelf();
        public event Action<IDestroyable> Destroyed;
    }
}
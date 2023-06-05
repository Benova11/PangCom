using System;
using UnityEngine;

namespace Game.Scripts.Collectables
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Collectable<T> : MonoBehaviour, IDestroyable
    {
        public abstract T Content { get; set; }
        
        public abstract void DestroySelf();
        public abstract event Action OnDestroyed;
        protected abstract void OnCollisionEnter2D(Collision2D other);
    }
}
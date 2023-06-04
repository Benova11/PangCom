using UnityEngine;

namespace Game.Scripts.Collectables
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Collectable<T> : MonoBehaviour, IDestroyable
    {
        public abstract T Content { get; set; }
        
        public abstract void Destroy();
        protected abstract void OnCollisionEnter2D(Collision2D other);
    }
}
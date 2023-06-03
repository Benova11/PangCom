using UnityEngine;

namespace Game.Scripts.Collectables
{
    public abstract class Collectable<T> : MonoBehaviour
    {
        public abstract T Content { get; set; }

        protected abstract void OnCollisionEnter2D(Collision2D other);
    }
}
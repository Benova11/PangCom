using System;
using Game.Scripts.Collectables;
using UnityEngine;

namespace Game.Scripts
{
    public class Obstacle : MonoBehaviour, IDestroyable
    {
        [SerializeField] private bool _isDestroyable;
        [SerializeField] private int _hitPointsToDestroy;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.TryGetComponent(out Projectile projectile);
                
            if (_isDestroyable && projectile != null)
            {
                _hitPointsToDestroy--;
                if (_hitPointsToDestroy == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        
        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        public event Action<IDestroyable> Destroyed;
    }
}
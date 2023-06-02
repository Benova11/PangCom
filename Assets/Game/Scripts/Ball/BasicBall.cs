using UnityEngine;

namespace Game.Scripts
{
    public class BasicBall : Ball
    {
        #region Editor Components

        [SerializeField] private SpriteRenderer _spriteRenderer;

        #endregion

        #region Methods

        public void SetBallVisuals(Sprite sprite, Vector2 localScale)
        {
            _spriteRenderer.sprite = sprite;
            _transform.localScale = localScale;
        }

        #endregion
    }
}
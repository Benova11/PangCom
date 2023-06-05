using System;
using Game.Scripts;
using UnityEngine;

namespace Game.Events
{
    public class DestroyBallEventArgs : EventArgs
    {
        private readonly Ball _ball;
        private readonly Transform _originTransform;
        
        public Ball Ball => _ball;
        public Transform OriginTransform => _originTransform;
        
        public DestroyBallEventArgs(Transform originTransform, Ball ball)
        {
            _ball = ball;
            _originTransform = originTransform;
        }
    }
}
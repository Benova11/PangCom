using System;
using Game.Scripts;
using UnityEngine;

namespace Game.Events
{
    public class DestroyEventArgs : EventArgs
    {
        public readonly Transform OriginTransform;
        public readonly Ball Ball;
        
        public DestroyEventArgs(Transform originTransform, Ball ball)
        {
            Ball = ball;
            OriginTransform = originTransform;
        }
    }
}
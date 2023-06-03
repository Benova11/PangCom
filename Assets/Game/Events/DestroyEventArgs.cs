using System;
using UnityEngine;

namespace Game.Events
{
    public class DestroyEventArgs : EventArgs
    {
        public readonly Transform OriginTransform;
        
        public DestroyEventArgs(Transform originTransform)
        {
            OriginTransform = originTransform;
        }
    }
}
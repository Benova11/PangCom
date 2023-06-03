using System;
using System.Collections.Generic;

namespace Game.Events
{
    public static class GameplayEventBus<T,E>
    {
        private static readonly IDictionary<T, List<Action<E>>> _subscriptions = new Dictionary<T, List<Action<E>>>();

        public static void Publish(T eventType, E args)
        {
            ValidateKeyExists(eventType);
            var existingSubscriptions = _subscriptions[eventType];
            foreach (var subscription in existingSubscriptions)
            {
                subscription.Invoke(args);
            }
        }

        public static void Subscribe(T eventType, Action<E> callback)
        {
            ValidateKeyExists(eventType);
            var existingSubscriptions = _subscriptions[eventType];
            existingSubscriptions.Add(callback);
        }

        public static void Unsubscribe(T eventType, Action<E> callback)
        {
            ValidateKeyExists(eventType);
            var existingSubscriptions = _subscriptions[eventType];
            existingSubscriptions.Remove(callback);
        }

        private static void ValidateKeyExists(T eventType)
        {
            if (!_subscriptions.ContainsKey(eventType))
            {
                _subscriptions.Add(eventType, new List<Action<E>>());
            }
        }
    }
}
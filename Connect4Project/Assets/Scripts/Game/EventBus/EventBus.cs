using System;

namespace Game {
    public class EventBus<T> where T : BusEvent
    {
        public static event Action<T> OnInvoke;

        public static void AddListener(Action<T> listener)
        {
            OnInvoke += listener;
        }

        public static void RemoveListener(Action<T> listener)
        {
            OnInvoke -= listener;
        }

        public static void Invoke(T pEvent)
        {
            OnInvoke?.Invoke(pEvent);
        }
    }
}


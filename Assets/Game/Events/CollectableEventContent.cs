namespace Game.Events
{
    public class CollectableEventContent<T>
    {
        public T Args { get; private set; }

        public CollectableEventContent(T args)
        {
            Args = args;
        }
    }
}
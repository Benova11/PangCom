namespace Game.Events
{
    public class CollectableEventArgs
    {
        public int Amount { get; private set; }

        public CollectableEventArgs(int amount)
        {
            Amount = amount;
        }
    }
}
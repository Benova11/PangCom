using Game.Scripts.Collectables;

namespace Game.Configs.Collectable
{
    public class RewardContent
    {
        public int Amount { get; set; }
        public IDestroyable Destroyable { get; set; }
    }
}
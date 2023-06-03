using Cysharp.Threading.Tasks;
using Game.Infrastructures.Factories.Balls;

namespace Game.Infrastructures.BallSpawners
{
    public interface IBallSpawner
    {
        UniTaskVoid Spawn(IBallFactory ballFactory);
    }
}
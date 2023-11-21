using GameFramework.Core.Position;

namespace GameFramework.Objects
{
    public interface IObject2D
    {
        IPosition2D Position { get; }
        bool IsObstacle { get; }
        void SteppedOn(IInteractableObject2D interactableObject2D);
    }
}
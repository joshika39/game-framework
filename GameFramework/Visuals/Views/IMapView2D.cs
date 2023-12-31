using System.Collections.ObjectModel;
using GameFramework.Objects.Interactable;
using GameFramework.Objects.Static;
using GameFramework.Visuals.Handlers;

namespace GameFramework.Visuals.Views
{
    public interface IMapView2D
    {
        public ObservableCollection<IStaticObject2D> MapObjects { get; }
        public ObservableCollection<IInteractableObject2D> InteractableObjects { get; }
        void Attach(IMouseHandler mouseHandler);
        void Clear();
    }
}

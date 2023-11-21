﻿using GameFramework.Core.Position;
using GameFramework.Visuals;
using GameFramework.Visuals.Tiles;
using GameFramework.Visuals.Views;

namespace GameFramework.UI.Forms.Tiles
{
    public abstract class AUnitTileView : AFocusableTileView, IMovingObjectView
    {
        private readonly ICollection<IViewLoadedSubscriber> _onLoadedSubscribers = new List<IViewLoadedSubscriber>();
        private readonly ICollection<IViewDisposedSubscriber> _onDisposedSubscribers = new List<IViewDisposedSubscriber>();
        
        protected AUnitTileView(IPosition2D position, double size, Color fillColor, bool hasBorder) : base(position, size, fillColor, hasBorder)
        { }
        
        public void UpdatePosition(IPosition2D position)
        {
            BeginInvoke(() =>
            {
                Left = (int)Math.Round(position.X * Size);
                Top = (int)Math.Round(position.Y * Size);
            });
        }
        
        public void ViewLoaded()
        {
            foreach (var subscriber in _onLoadedSubscribers)
            {
                subscriber.OnLoaded();
            }
        }
        
        public virtual void Attach(IViewLoadedSubscriber subscriber)
        {
            _onLoadedSubscribers.Add(subscriber);
        }

        public virtual void Attach(IViewDisposedSubscriber subscriber)
        {
            _onDisposedSubscribers.Add(subscriber);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                foreach (var subscriber in _onDisposedSubscribers)
                {
                    subscriber.OnViewDisposed(this);
                }
            }
        }
    }
}
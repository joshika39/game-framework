﻿using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Entities;
using GameFramework.GameFeedback;
using GameFramework.Impl.GameFeedback;
using GameFramework.Manager;
using GameFramework.Tiles;
using GameFramework.UI.Forms.Tiles;

namespace GameFramework.Forms.Game.Tiles
{
    internal class HoleTile : ATile, IDeadlyTile
    {
        private readonly IGameManager _gameManager;
        public override bool IsObstacle => false;
        
        public HoleTile(IPosition2D position, IConfigurationService2D configurationService, IGameManager gameManager) : base(position, configurationService, Color.Black)
        {
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
        }

        public override void SteppedOn(IUnit2D unit2D)
        {
            if (_gameManager.State == GameState.InProgress)
            {
                unit2D.Step(this);
                _gameManager.EndGame(new GameplayFeedback(FeedbackLevel.Info, "You died!"), GameResolution.Loss);
            }
        }
    }
}
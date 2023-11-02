using System.Diagnostics;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.GameFeedback;
using GameFramework.Map;
using GameFramework.Time;

namespace GameFramework.Impl.Core
{
    internal class GameManager : IGameManager
    {
        private readonly IConfigurationService2D _configurationService;
        private readonly ICollection<IGameManagerSubscriber> _listeners;

        public GameState State { get; private set; }
        public IStopwatch Timer { get; }
        
        public GameManager(IStopwatch stopwatch, IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            Timer = stopwatch ?? throw new ArgumentNullException(nameof(stopwatch));
            _listeners = new List<IGameManagerSubscriber>();
            State = GameState.NotStarted;
        }
        
        public void StartGame<T>(IGameplayFeedback feedback, T map2D) where T : IMap2D
        {
            if(State is GameState.InProgress)
            {
                Debug.WriteLine("Game is already in progress. Cannot start.");
                return;
            }
            
            Timer.Start();
            _configurationService.SetActiveMap(map2D);
            foreach (var gameFeedbackListener in _listeners)
            {
                gameFeedbackListener.OnGameStarted(feedback);
            }
            State = GameState.InProgress;
        }
        
        public void EndGame(IGameplayFeedback feedback, GameResolution resolution)
        {
            if(State is GameState.Finished or GameState.NotStarted)
            {
                Debug.WriteLine("Game is not in progress. Cannot end.");
                return;
            }
            
            State = GameState.Finished;
            Timer.Stop();
            foreach (var gameFeedbackListener in _listeners)
            {
                gameFeedbackListener.OnGameFinished(feedback, resolution);
            }
        }
        
        public void PauseGame()
        {
            if (State is GameState.Finished or GameState.NotStarted)
            {
                Debug.WriteLine("Game is not in progress. Cannot pause.");
                return;
            }
            Timer.Stop();
            foreach (var gameFeedbackListener in _listeners)
            {
                gameFeedbackListener.OnGamePaused();
            }
            State = GameState.Paused;
        }
        
        public void ResumeGame()
        {
            if (State is GameState.Finished or GameState.NotStarted)
            {
                Debug.WriteLine("Game is not in progress or paused. Cannot resume.");
                return;
            }
            
            Timer.Start();
            foreach (var gameFeedbackListener in _listeners)
            {
                gameFeedbackListener.OnGameResumed();
            }
            State = GameState.InProgress;
        }
        
        public void ResetGame()
        {
            if (State is GameState.NotStarted)
            {
                Debug.WriteLine("Game is not in progress. Cannot reset.");
                return;
            }
            
            Timer.Reset();
            foreach (var gameFeedbackListener in _listeners)
            {
                gameFeedbackListener.OnGameReset();
            }
            State = GameState.NotStarted;
        }

        public void AttachListener(IGameManagerSubscriber listener)
        {
            _listeners.Add(listener);
        }
    }
}
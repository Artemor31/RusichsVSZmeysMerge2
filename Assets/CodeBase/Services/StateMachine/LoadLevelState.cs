﻿using Services.GridService;
using Services.Infrastructure;
using UI;

namespace Services.StateMachine
{
    public class LoadLevelState : IExitableState
    {
        private const string GameplaySceneName = "Gameplay";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly WaveBuilder _waveBuilder;
        private readonly GridLogicService _gridLogicService;
        private readonly WindowsService _windowsService;

        public LoadLevelState(GameStateMachine gameStateMachine,
                              SceneLoader sceneLoader,
                              WaveBuilder waveBuilder,
                              GridLogicService gridLogicService,
                              WindowsService windowsService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _waveBuilder = waveBuilder;
            _gridLogicService = gridLogicService;
            _windowsService = windowsService;
        }

        public void Enter()
        {
            _windowsService.Show<LoadingScreen>();
            _sceneLoader.Load(GameplaySceneName, then: () => _gameStateMachine.Enter<SetupLevelState>());
        }

        public void Exit()
        {
            _gridLogicService.CreatePlayerField();
            _waveBuilder.BuildEnemyWave();
            _windowsService.Close<LoadingScreen>();
        }
    }
}
﻿using CodeBase.Infrastructure;
using CodeBase.Services.StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public abstract class BaseResultPresenter : Presenter
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private Button _nextLevel;

        private GameStateMachine _gameStateMachine;

        public override void Init()
        {
            _gameStateMachine = ServiceLocator.Resolve<GameStateMachine>();
            _moneyText.text = 100.ToString();
            _nextLevel.onClick.AddListener(OnNextLevelClicked);
        }

        private void OnNextLevelClicked()
        {
            gameObject.SetActive(false);
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}
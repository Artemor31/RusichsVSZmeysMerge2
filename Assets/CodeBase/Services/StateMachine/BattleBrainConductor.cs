﻿using System.Collections.Generic;
using System.Linq;
using CodeBase.Databases;
using CodeBase.Gameplay.Units;
using CodeBase.Models;

namespace CodeBase.Services.StateMachine
{
    public class BattleBrainConductor
    {
        private readonly GameplayModel _gameplayModel;
        private readonly BrainsDatabase _brainsDatabase;

        public BattleBrainConductor(GameplayModel gameplayModel, BrainsDatabase brainsDatabase)
        {
            _gameplayModel = gameplayModel;
            _brainsDatabase = brainsDatabase;
        }

        public void StartBattle()
        {
            FindBestTargets(_gameplayModel.PlayerUnits, _gameplayModel.EnemyUnits);
            FindBestTargets(_gameplayModel.EnemyUnits, _gameplayModel.PlayerUnits);
        }

        private void FindBestTargets(List<Unit> seekers, List<Unit> candidates)
        {
            foreach (var unit in seekers)
            {
                var brain = _brainsDatabase.Brains.First(x => x.UnitId == unit.Id);
                var bestTarget = brain.Brain.BestTarget(candidates);
                unit.SetTarget(bestTarget);
            }
        }
    }
}
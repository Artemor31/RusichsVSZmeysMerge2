﻿using System.Linq;
using Databases;
using Databases.Data;
using Infrastructure;
using Services;
using UnityEngine;

namespace Gameplay.Units
{
    public class RangerActor : Actor
    {
        [SerializeField] private ProjectileType _projectileType; 
        private ProjectileService _service;

        public override void Initialize(ActorSkin view, ActorData data, ActorStats stats)
        {
            _service = ServiceLocator.Resolve<ProjectileService>();
            base.Initialize(view, data, stats);
        }

        protected override void Tick()
        {
            if (IsDead) return;

            TickActTimer();

            if (!CanFindTarget()) return;

            transform.LookAt(Target.transform);

            if (InRange())
            {
                _mover.Stop();

                if (CooldownUp)
                {
                    PerformAct();
                }
            }
            else
            {
                if (CooldownUp)
                {
                    _mover.MoveTo(Target);
                }
            }
        }

        private void PerformAct()
        {
            View.PerformAct();
            float damage = Random.Range(0, 1f) <= Stats.CritChance
                ? Stats.Damage * (1 + Stats.CritValue)
                : Stats.Damage;

            Vector3 position = transform.position + Vector3.up;
            _service.Create(_projectileType, position, Target, damage);
            
            ResetCooldown();
        }

        protected override bool NeedNewTarget() => Target == null || Target.IsDead;

        protected override void SearchNewTarget() => Target = SearchTarget.For(this)
                                                                          .SelectTargets(Side.Enemy)
                                                                          .FilterBy(Strategy.OnSameLine)
                                                                          .FirstOrDefault();
    }
}
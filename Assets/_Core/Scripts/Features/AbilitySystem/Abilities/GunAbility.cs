﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.AbilitySystem.Abilities
{
    internal class GunAbility : IAbility
    {
        private readonly IAbilityItem _config;
        private List<GameObject> _projectileList = new List<GameObject>();


        public GunAbility([NotNull] IAbilityItem config) =>
            _config = config ?? throw new ArgumentNullException(nameof(config));

        public void Apply(IAbilityActivator activator)
        {
            var projectile = Object.Instantiate(_config.Projectile).GetComponent<Rigidbody2D>();
            Vector3 force = activator.ViewGameObject.transform.right * _config.Value;
            projectile.AddForce(force, ForceMode2D.Force);
            _projectileList.Add(projectile.gameObject);
        }

        public void Dispose()
        {
            foreach (GameObject projectile in _projectileList)
            {
                Object.Destroy(projectile);
            }
            _projectileList.Clear();
        }
    }
}
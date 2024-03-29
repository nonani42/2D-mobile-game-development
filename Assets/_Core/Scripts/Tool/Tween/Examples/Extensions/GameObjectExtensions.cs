﻿using UnityEngine;

namespace Tween.Examples
{
    internal static class GameObjectExtensions
    {
        public static void Move(this GameObject go, Vector3 startPosition, Vector3 endPosition, float duration)
        {
            if (!go.TryGetComponent(out VectorLerpComponent component))
                component = go.AddComponent<VectorLerpComponent>();

            component.Play(startPosition, endPosition, duration);
        }
    }
}

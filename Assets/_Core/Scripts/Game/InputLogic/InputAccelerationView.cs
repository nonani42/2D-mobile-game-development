﻿using UnityEngine;

namespace CarGame
{
    public class InputAccelerationView : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 0.05f;

        protected override void Move()
        {
            Vector3 direction = CalcDirection();
            float moveValue = _speed * _inputMultiplier * Time.deltaTime * direction.x;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
            {
                OnRightMove(abs);
            }
            else
            {
                OnLeftMove(abs);
            }
        }

        private Vector3 CalcDirection()
        {
            const float normalizedMagnitude = 1f;

            Vector3 direction = Vector3.zero;
            direction.x = Input.acceleration.x;
            direction.z = -Input.acceleration.x;

            if(direction.sqrMagnitude > normalizedMagnitude)
            {
                direction.Normalize();
            }

            return direction;
        }
    }
}

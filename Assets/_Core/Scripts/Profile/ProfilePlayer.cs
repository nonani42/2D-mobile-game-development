﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CarGame
{
    public class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly CarModel CurrentCar;

        public ProfilePlayer(float speedCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar);
        }

        public ProfilePlayer(float speedCar, GameState initialState) : this(speedCar)
        {
            CurrentState.Value = initialState;
        }
    }
}
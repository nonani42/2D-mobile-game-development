using CarGame;
using Features.Inventory;
using Features.Rewards.Currency;

namespace Profile
{
    internal class PlayerProfile
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly CarModel CurrentCar;
        public readonly InventoryModel Inventory;
        public readonly CurrencyModel Currency;


        public PlayerProfile(float speedCar, float jumpHeight)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar, jumpHeight);
            Inventory = new InventoryModel();
            Currency = new CurrencyModel();
        }

        public PlayerProfile(float speedCar, float jumpHeight, GameState initialState) : this(speedCar, jumpHeight)
        {
            CurrentState.Value = initialState;
        }
    }
}
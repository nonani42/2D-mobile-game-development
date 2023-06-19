using Features.Shed.Upgrade;

namespace CarGame
{
    public class CarModel : IUpgradable
    {
        private readonly float _defaultSpeed;
        private readonly float _defaultJump;

        public float Speed { get; set; }
        public float Jump { get; set; }

        public CarModel(float speed)
        {
            _defaultSpeed = speed;
            Speed = speed;
            _defaultJump = 0f;
        }

        public void Restore()
        {
            Speed = _defaultSpeed;
            Jump = _defaultJump;
        }
    }
}
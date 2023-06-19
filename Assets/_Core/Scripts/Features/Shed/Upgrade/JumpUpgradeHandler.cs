namespace Features.Shed.Upgrade
{
    internal class JumpUpgradeHandler : IUpgradeHandler
    {
        private readonly float _jumpHeight;

        public JumpUpgradeHandler(float jumpHeight) =>
            _jumpHeight = jumpHeight;

        public void Upgrade(IUpgradable upgradable) =>
            upgradable.Jump += _jumpHeight;
    }
}
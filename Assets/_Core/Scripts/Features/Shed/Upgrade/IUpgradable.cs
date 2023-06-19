namespace Features.Shed.Upgrade
{
    internal interface IUpgradable
    {
        float Speed { get; set; }
        float Jump { get; set; }
        void Restore();
    }
}
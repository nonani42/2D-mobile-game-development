using System.Collections.Generic;

namespace Features.Shed.Upgrade
{
    internal interface IUpgradeHandlersRepository : IRepository
    {
        IReadOnlyDictionary<string, IUpgradeHandler> Items { get; }
    }

    internal class UpgradeHandlersRepository
        : BaseRepository<string, IUpgradeHandler, UpgradeItemConfig>, IUpgradeHandlersRepository
    {
        public UpgradeHandlersRepository(IEnumerable<UpgradeItemConfig> configs) : base(configs)
        { }

        protected override string GetKey(UpgradeItemConfig config) =>
            config.Id;

        protected override IUpgradeHandler CreateItem(UpgradeItemConfig config) 
        {
            switch (config.Type)
            {
                case UpgradeType.Speed: 
                    return new SpeedUpgradeHandler(config.Value);
                case UpgradeType.JumpHeight:
                    return new JumpUpgradeHandler(config.Value);
                default:
                    return _ = StubUpgradeHandler.Default;
            }
        }
    }
}
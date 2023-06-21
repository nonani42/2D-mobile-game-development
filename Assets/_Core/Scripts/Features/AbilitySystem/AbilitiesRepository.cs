using System.Collections.Generic;
using Features.AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesRepository : IRepository
    {
        IReadOnlyDictionary<string, IAbility> Items { get; }
    }

    internal class AbilitiesRepository : BaseRepository<string, IAbility, IAbilityItem>, IAbilitiesRepository
    {
        public AbilitiesRepository(IEnumerable<IAbilityItem> abilityItems) : base(abilityItems)
        { }

        protected override string GetKey(IAbilityItem config) => config.Id;

        protected override IAbility CreateItem(IAbilityItem abilityItem)
        {
            switch(abilityItem.Type) 
            {
                case AbilityType.Gun: return new GunAbility(abilityItem);
                case AbilityType.Jump: return new JumpAbility(abilityItem);
                default: return _ = StubAbility.Default;
            }
        }
    }
}

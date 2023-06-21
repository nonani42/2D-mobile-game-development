using Features.AbilitySystem.Abilities;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesController
    { }

    internal class AbilitiesController : BaseController, IAbilitiesController
    {
        private readonly IAbilitiesView _abilityView;
        private readonly IAbilitiesRepository _abilityRepository;
        private readonly IAbilityActivator _abilityActivator;

        public AbilitiesController(
            [NotNull] IAbilitiesView abilityView,
            [NotNull] IAbilitiesRepository abilityRepository,
            [NotNull] IAbilityActivator abilityActivator,
            [NotNull] IEnumerable<IAbilityItem> items)
        {
            _abilityView =
                abilityView ?? throw new ArgumentNullException(nameof(abilityView));

            _abilityRepository =
                abilityRepository ?? throw new ArgumentNullException(nameof(abilityRepository));

            _abilityActivator = 
                abilityActivator ?? throw new ArgumentNullException(nameof(abilityActivator));

                if(items == null)
                    throw new ArgumentNullException(nameof(items));

            _abilityView.Display(items, OnAbilityViewClicked);
        }

        private void OnAbilityViewClicked(string abilityId)
        {
            if (_abilityRepository.Items.TryGetValue(abilityId, out IAbility ability))
                ability.Apply(_abilityActivator);
        }
    }
}

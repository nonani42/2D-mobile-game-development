using UnityEngine;

namespace Rewards
{
    internal class CurrencyContainerView : MonoBehaviour
    {
        public Transform placeForCurrencySlots;

        public void AddWood(CurrencySlotView currency, int value)
        {
            currency.SetData(value);
        }
    }
}

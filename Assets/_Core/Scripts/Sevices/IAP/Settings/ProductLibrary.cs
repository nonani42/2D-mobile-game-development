using UnityEngine;

namespace CarGame
{
    [CreateAssetMenu(fileName = nameof(ProductLibrary), menuName = "Settings/IAP/" + nameof(ProductLibrary))]
    internal class ProductLibrary : ScriptableObject
    {
        [field: SerializeField] public Product[] Products { get; private set; }
    }
}

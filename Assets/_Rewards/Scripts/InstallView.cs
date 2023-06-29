using UnityEngine;

namespace Rewards
{
    internal class InstallView : MonoBehaviour
    {
        [field: Header("Windows Prefabs")]
        [SerializeField] private GameObject _currencyWindowPrefab;
        [SerializeField] private GameObject _rewardWindowPrefab;

        [field: Header("Configs")]
        [SerializeField] private CurrencySlotConfigDataSource _currencySlotConfigDataSource;
        [SerializeField] private RewardSlotConfigDataSource _rewardSlotConfigDataSource;

        private GameObject _currencyWindow;
        private GameObject _rewardWindow;

        private CurrencyContainerView _currencyContainerView;
        private RewardContainerView _rewardView;

        private RewardContainerController _rewardController;
        private CurrencyContainerController _currencyController;


        private void Awake()
        {
            _currencyWindow = Instantiate(_currencyWindowPrefab, gameObject.transform.position, Quaternion.identity);
            _currencyContainerView = _currencyWindow.GetComponent<CurrencyContainerView>();
            _rewardWindow = Instantiate(_rewardWindowPrefab, gameObject.transform.position, Quaternion.identity);
            _rewardView = _rewardWindow.GetComponent<RewardContainerView>();

            _currencyController = new CurrencyContainerController(_currencyContainerView, _currencySlotConfigDataSource);
            _rewardController = new RewardContainerController(_rewardView, _currencyController, _rewardSlotConfigDataSource);
        }

        private void Start()
        {
            _currencyController.Init();
            _rewardController.Init();
        }

        private void OnDestroy()
        {
            _currencyController.Deinit();
            _rewardController.Deinit();
        }
    }
}
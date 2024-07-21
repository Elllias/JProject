using Core.Entity;
using Core.UI.PlayerScore;
using Mirror;
using UnityEngine;

namespace Core
{
    public class ApplicationBootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerScoreView _playerScoreView;
        [SerializeField] private AdvancedNetworkManager _networkManager;
        
        [Header("Cube factory data")]
        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private Transform _worldTransform;

        private void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            var playerScoreViewController = new PlayerScoreViewController(_playerScoreView);
            ServiceLocator.Bind(playerScoreViewController);

            ServiceLocator.Bind(_networkManager);

            var cubeFactory = new CubeFactory(_cubePrefab, _worldTransform);
            ServiceLocator.Bind(cubeFactory);
        }
    }
}
using Source.Features.User;
using Source.Services.AssetBundle;
using Source.Services.ServicesResolver;
using UnityEngine;

namespace Source.Features.Gameplay.EndGame
{
    public class LoseStrategy : BaseGameOverStrategy
    {
        public LoseStrategy(UserModel userModel, ServiceResolver serviceResolver) : base(userModel, serviceResolver)
        {
        }
        
        public override void Execute()
        {
            DeductPlayerPoints();
            
            ShowGameOverUI();
            
            PlayLoseSound();
        }

        private async void ShowGameOverUI()
        {
            if (_userModel.Lives == 0)
            {
                Debug.LogError("TODO: not enough lives -> display shop to buy more...");
            }
            
            var gameOverAsset = await _serviceResolver.Get<IAssetBundleService>().LoadAsset<GameOverView>("GameLostView");
            Object.Instantiate(gameOverAsset);
        }

        private void DeductPlayerPoints()
        {
            _userModel.DeductLives();
        }

        private void PlayLoseSound()
        {
            // TODO: Logic to play a losing sound effect
        }
    }

}
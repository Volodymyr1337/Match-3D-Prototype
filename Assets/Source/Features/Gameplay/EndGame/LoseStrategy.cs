using Source.Features.User;
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
            ShowGameOverUI();

            DeductPlayerPoints();

            PlayLoseSound();
        }

        private void ShowGameOverUI()
        {
            
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
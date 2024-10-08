using Source.Features.User;
using UnityEngine;

namespace Source.Features.Gameplay.EndGame
{
    public class LoseStrategy : IGameOverStrategy
    {
        private UserModel _userModel;
        
        public LoseStrategy(UserModel userModel)
        {
            _userModel = userModel;
        }
        
        public void Execute()
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
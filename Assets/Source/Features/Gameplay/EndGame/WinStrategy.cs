using Source.Features.User;
using UnityEngine;

namespace Source.Features.Gameplay.EndGame
{
    public class WinStrategy : IGameOverStrategy
    {
        private UserModel _userModel;
        
        public WinStrategy(UserModel userModel)
        {
            _userModel = userModel;
        }
        
        public void Execute()
        {
            ShowVictoryUI();

            IncreasePlayerScore();
        
            PlayWinSound();
        }

        private void ShowVictoryUI()
        {
            
        }

        private void IncreasePlayerScore()
        {
            _userModel.IncreaseLevel();
        }

        private void PlayWinSound()
        {
            // TODO: Logic to play a winning sound effect
        }
    }

}
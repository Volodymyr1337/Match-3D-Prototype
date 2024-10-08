using Source.Features.User;
using Source.Services.ServicesResolver;

namespace Source.Features.Gameplay.EndGame
{
    public class WinStrategy : BaseGameOverStrategy
    {
        public WinStrategy(UserModel userModel, ServiceResolver serviceResolver) : base(userModel, serviceResolver)
        {
        }
        
        public override void Execute()
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
using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.User;

namespace Source.Features.Gameplay.EndGame
{
    public class EndGameController : BaseController
    {
        private UserModel _userModel;
        
        public EndGameController(UserModel userModel)
        {
            _userModel = userModel;
        }
        
        public override UniTask Initialize()
        {
            GameplayController.OnGameOver += OnGameOver;
            return UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            GameplayController.OnGameOver -= OnGameOver;
            base.Dispose();
        }

        private void OnGameOver(bool hasWon)
        {
            IGameOverStrategy gameOverStrategy;

            if (hasWon)
            {
                gameOverStrategy = new WinStrategy(_userModel, ServiceResolver);
            }
            else
            {
                gameOverStrategy = new LoseStrategy(_userModel, ServiceResolver);
            }

            gameOverStrategy.Execute();
        }
    }
}
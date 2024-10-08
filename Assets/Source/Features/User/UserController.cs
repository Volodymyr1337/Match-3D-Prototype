using Cysharp.Threading.Tasks;
using Source.Application;
using Source.Features.Gameplay;

namespace Source.Features.User
{
    public class UserController : BaseController
    {
        public UserModel UserModel { get; private set; }

        public override UniTask Initialize()
        {
            // TODO load user data
            UserModel = new UserModel();
            
            GameplayController.OnGameWon += OnGameWon;
            return UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            GameplayController.OnGameWon -= OnGameWon;
            
            base.Dispose();
        }

        private void OnGameWon()
        {
            UserModel.IncreaseLevel();
        }
    }
}
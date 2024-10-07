using Application;
using Cysharp.Threading.Tasks;

namespace Source.Features.Gameplay
{
    public class GameplayController : BaseController
    {

        public override async UniTask Initialize()
        {
            await StartGame();
        }

        private  UniTask StartGame()
        {
            return UniTask.CompletedTask;
        }
    }
}
using Cysharp.Threading.Tasks;
using Source.Application;

namespace Source.Features.Gameplay
{
    public class GameplayController : BaseController
    {
        private BoardController _boardController;
        public override async UniTask Initialize()
        {
            _boardController = new BoardController();
            await _boardController.Initialize();
        }
        
        private UniTask StartGame()
        {
            _boardController.GenerateBoard();
            
            return UniTask.CompletedTask;
        }
    }
}